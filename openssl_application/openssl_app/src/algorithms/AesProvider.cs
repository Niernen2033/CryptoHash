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
    class AesProvider
    {
        [DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeAesEncrypt(int aesType, byte[] key, uint keyBytes, byte[] msg, uint msgBytes,
			byte[] iv, uint ivBytes, out crypto_buffer_t digset);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeAesDecrypt(int aesType, byte[] key, uint keyBytes, byte[] msg, uint msgBytes,
			byte[] iv, uint ivBytes, out crypto_buffer_t digset);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeKeyWrap(byte[] key, uint keyBytes, byte[] msg, uint msgBytes,
			byte[] iv, uint ivBytes, out crypto_buffer_t digset);

		[DllImport(DllProperties.DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
		private static extern int computeKeyUnwrap(byte[] key, uint keyBytes, byte[] msg, uint msgBytes,
			byte[] iv, uint ivBytes, out crypto_buffer_t digset);

		public byte[] Hash { get; private set; }
		public AES_TYPE Type { get; set; }
		public byte[] Key { get; set; }
		public byte[] Msg { get; set; }
		public byte[] Iv { get; set; }

		public AesProvider()
		{
			this.Hash = null;
			this.Type = AES_TYPE.AES_CBC_256;
			this.Msg = null;
			this.Key = null;
			this.Iv = null;
		}

		public AesProvider(AES_TYPE type)
		{
			this.Hash = null;
			this.Type = type;
			this.Msg = null;
			this.Key = null;
			this.Iv = null;
		}

		public CRYPTO_STATUS ComputeDecrypt()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeAesDecrypt((int)this.Type, this.Key, (uint)this.Key.Length,
					this.Msg, (uint)this.Msg.Length, 
					this.Iv, (uint)this.Iv.Length, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "Decrypt failed", exc.Message);
			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
			}

			return result;
		}

		public CRYPTO_STATUS ComputeEncrypt()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeAesEncrypt((int)this.Type, this.Key, (uint)this.Key.Length,
					this.Msg, (uint)this.Msg.Length,
					this.Iv, (uint)this.Iv.Length, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "Encrypt failed", exc.Message);
			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
			}

			return result;
		}

		public CRYPTO_STATUS ComputeKeyWrap()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeKeyWrap(this.Key, (uint)this.Key.Length,
					this.Msg, (uint)this.Msg.Length,
					this.Iv, (uint)this.Iv.Length, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "KeyWrap failed", exc.Message);
			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
			}

			return result;
		}

		public CRYPTO_STATUS ComputeKeyUnwrap()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeKeyUnwrap(this.Key, (uint)this.Key.Length,
					this.Msg, (uint)this.Msg.Length,
					this.Iv, (uint)this.Iv.Length, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception exc)
			{
				LogManager.ShowMessageBox("Error", "KeyUnwrap failed", exc.Message);
			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.Hash = hash.buffer.Take((int)hash.bytes).ToArray();
			}

			return result;
		}
	}
}
