using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using openssl_app.dllmanager;
using System.Collections.ObjectModel;

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


		private List<byte> m_randomData;
		public SHA_TYPE Type { get; set; }
		public bool PredictionResistance { get; set; }
		public List<byte> PersonalizationString { get; set; }
		public List<byte> EntropyInput { get; set; }
		public List<byte> Nonce { get; set; }
		public List<byte> EntropyInputReseed { get; set; }
		public List<byte> AdditionalInputReseed { get; set; }
		public List<byte> AdditionalInput1 { get; set; }
		public List<byte> AdditionalInput2 { get; set; }
		public List<byte> EntropyInputPR1 { get; set; }
		public List<byte> EntropyInputPR2 { get; set; }
		public uint ReturnedBytes { get; set; }
		public ReadOnlyCollection<byte> RandomData { get { return this.m_randomData.AsReadOnly(); } }

		public HmacDrbgProvider()
		{
			this.m_randomData = new List<byte>();
			this.Type = SHA_TYPE.SHA_256;
			this.PredictionResistance = true;
			this.PersonalizationString = new List<byte>();
			this.EntropyInput = new List<byte>();
			this.Nonce = new List<byte>();
			this.EntropyInputReseed = new List<byte>();
			this.AdditionalInputReseed = new List<byte>();
			this.AdditionalInput1 = new List<byte>();
			this.AdditionalInput2 = new List<byte>();
			this.EntropyInputPR1 = new List<byte>();
			this.EntropyInputPR2 = new List<byte>();
			this.ReturnedBytes = 0;
		}

		private CRYPTO_STATUS Instantiate()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			try
			{
				int computeStatus = computeHmacDrbg_Instantiate(this.PredictionResistance, (int)this.Type,
					this.PersonalizationString.ToArray(), (uint)this.PersonalizationString.Count,
					this.EntropyInput.ToArray(), (uint)this.EntropyInput.Count,
					this.Nonce.ToArray(), (uint)this.Nonce.Count);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}
			return result;
		}

		private CRYPTO_STATUS Reseed()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			try
			{
				int computeStatus = computeHmacDrbg_Reseed(this.PredictionResistance,
					this.EntropyInput.ToArray(), (uint)this.EntropyInput.Count,
					this.AdditionalInputReseed.ToArray(), (uint)this.AdditionalInputReseed.Count);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}
			return result;
		}

		private CRYPTO_STATUS Generate(List<byte> entropy, List<byte> additionalInput)
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t randomData = new crypto_buffer_t();
			try
			{
				uint entropyCount = (entropy == null) ? 0 : (uint)entropy.Count;
				uint additionalInputCount = (additionalInput == null) ? 0 : (uint)additionalInput.Count;
				int computeStatus = computeHmacDrbg_Generate(this.ReturnedBytes,
					entropy.ToArray(), entropyCount,
					additionalInput.ToArray(), additionalInputCount,
					out randomData);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}
			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.m_randomData = new List<byte>(randomData.buffer.Take((int)randomData.bytes));
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
			catch (Exception e)
			{

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
