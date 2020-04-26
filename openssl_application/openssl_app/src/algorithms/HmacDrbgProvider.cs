using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using openssl_app.dllmanager;
using openssl_app.logmanager;

namespace openssl_app.algorithms
{
    class HmacDrbgProvider
    {
		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeHmacDrbg_Instantiate(bool requestPredictionResistance, int shaType,
			byte[] personalizationString, uint personalizationStringBytes,
			byte[] entropy, uint entropyBytes,
			byte[] nonce, uint nonceBytes);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeHmacDrbg_Reseed(bool requestPredictionResistance,
			byte[] entropy, uint entropyBytes,
			byte[] additionalInput, uint additionalInputBytes);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeHmacDrbg_Generate(uint bytesRequested,
			byte[] entropy, uint entropyBytes,
			byte[] additionalInput, uint additionalInputBytes,
			out crypto_buffer_t bytesReturned);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeHmacDrbg_Uninstantiate();


		public byte[] RandomData { get; private set; }
		public SHA_TYPE Type { get; set; }
		public bool PredictionResistance { get; set; }
		public byte[] PersonalizationString { get; set; }
		public byte[] EntropyInput { get; set; }
		public byte[] Nonce { get; set; }
		public byte[] EntropyInputReseed { get; set; }
		public byte[] AdditionalInputReseed { get; set; }
		public byte[] AdditionalInput1 { get; set; }
		public byte[] AdditionalInput2 { get; set; }
		public byte[] EntropyInputPR1 { get; set; }
		public byte[] EntropyInputPR2 { get; set; }
		public uint ReturnedBytes { get; set; }

		public HmacDrbgProvider()
		{
			this.RandomData = null;
			this.Type = SHA_TYPE.SHA_256;
			this.PredictionResistance = true;
			this.PersonalizationString = null;
			this.EntropyInput = null;
			this.Nonce = null;
			this.EntropyInputReseed = null;
			this.AdditionalInputReseed = null;
			this.AdditionalInput1 = null;
			this.AdditionalInput2 = null;
			this.EntropyInputPR1 = null;
			this.EntropyInputPR2 = null;
			this.ReturnedBytes = 0;
		}

		private CRYPTO_STATUS Instantiate()
		{
			if (this.PersonalizationString == null || this.EntropyInput == null || this.Nonce == null)
			{
				return CRYPTO_STATUS.CRYPTO_NULL_PTR_ERROR;
			}

			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			try
			{
				int computeStatus = computeHmacDrbg_Instantiate(this.PredictionResistance, (int)this.Type,
					this.PersonalizationString, (uint)this.PersonalizationString.Length,
					this.EntropyInput, (uint)this.EntropyInput.Length,
					this.Nonce, (uint)this.Nonce.Length);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "DrbgInstantiate failed", exc.Message);
			}
			return result;
		}

		private CRYPTO_STATUS Reseed()
		{
			if (this.EntropyInput == null || this.AdditionalInputReseed == null)
			{
				return CRYPTO_STATUS.CRYPTO_NULL_PTR_ERROR;
			}

			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			try
			{
				int computeStatus = computeHmacDrbg_Reseed(this.PredictionResistance,
					this.EntropyInput, (uint)this.EntropyInput.Length,
					this.AdditionalInputReseed, (uint)this.AdditionalInputReseed.Length);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "DrbgReseed failed", exc.Message);
			}
			return result;
		}

		private CRYPTO_STATUS Generate(byte[] entropy, byte[] additionalInput)
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t randomData = new crypto_buffer_t();
			try
			{
				uint entropyCount = (entropy == null) ? 0 : (uint)entropy.Length;
				uint additionalInputCount = (additionalInput == null) ? 0 : (uint)additionalInput.Length;
				int computeStatus = computeHmacDrbg_Generate(this.ReturnedBytes,
					entropy, entropyCount,
					additionalInput, additionalInputCount,
					out randomData);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "DrbgGenerate failed", exc.Message);
			}
			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.RandomData = randomData.buffer.Take((int)randomData.bytes).ToArray();
			}
			return result;
		}

		private CRYPTO_STATUS Uninstantiate()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			try
			{
				int computeStatus = computeHmacDrbg_Uninstantiate();
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "DrbgUninstantiate failed", exc.Message);
			}
			return result;
		}

		public CRYPTO_STATUS GenerateRandomData()
		{
			CRYPTO_STATUS status;
			if (this.PredictionResistance == false)
			{
				//preRes = FALSE
				//1. Hmac_Drgb_Instantiate
				//2. Hmac_Drgb_Reseed
				//3. Hmac_Drgb_Generate
				//4. Hmac_Drgb_Generate
				status = this.Instantiate();
				if(status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Reseed();
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Generate(null, this.AdditionalInput1);
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Generate(null, this.AdditionalInput2);
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Uninstantiate();
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
			}
			else
			{
				//preRes = TRUE
				//1. Hmac_Drgb_Instantiate
				//2. Hmac_Drgb_Generate
				//3. Hmac_Drgb_Generate
				status = this.Instantiate();
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Generate(this.EntropyInputPR1, this.AdditionalInput1);
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Generate(this.EntropyInputPR2, this.AdditionalInput2);
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
				status = this.Uninstantiate();
				if (status != CRYPTO_STATUS.CRYPTO_SUCCESS)
				{
					return status;
				}
			}

			return CRYPTO_STATUS.CRYPTO_SUCCESS;
		}
	}
}
