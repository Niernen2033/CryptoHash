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

		private List<byte> m_hash;
		public AES_TYPE Type { get; set; }
		public List<byte> Key { get; set; }
		public List<byte> Msg { get; set; }
		public List<byte> Iv { get; set; }
		public ReadOnlyCollection<byte> Hash { get { return this.m_hash.AsReadOnly(); } }

		public AesProvider()
		{
			this.m_hash = new List<byte>();
			this.Type = AES_TYPE.AES_CBC_256;
			this.Msg = new List<byte>();
			this.Key = new List<byte>();
			this.Iv = new List<byte>();
		}

		public AesProvider(AES_TYPE type)
		{
			this.m_hash = new List<byte>();
			this.Type = type;
			this.Msg = new List<byte>();
			this.Key = new List<byte>();
			this.Iv = new List<byte>();
		}

		public CRYPTO_STATUS ComputeDecrypt()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeAesDecrypt((int)this.Type, this.Key.ToArray(), (uint)this.Key.Count,
					this.Msg.ToArray(), (uint)this.Msg.Count, 
					this.Iv.ToArray(), (uint)this.Iv.Count, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
			}

			return result;
		}

		public CRYPTO_STATUS ComputeEncrypt()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeAesEncrypt((int)this.Type, this.Key.ToArray(), (uint)this.Key.Count,
					this.Msg.ToArray(), (uint)this.Msg.Count,
					this.Iv.ToArray(), (uint)this.Iv.Count, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
			}

			return result;
		}

		public CRYPTO_STATUS ComputeKeyWrap()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeKeyWrap(this.Key.ToArray(), (uint)this.Key.Count,
					this.Msg.ToArray(), (uint)this.Msg.Count,
					this.Iv.ToArray(), (uint)this.Iv.Count, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
			}

			return result;
		}

		public CRYPTO_STATUS ComputeKeyUnwrap()
		{
			CRYPTO_STATUS result = CRYPTO_STATUS.CRYPTO_ERROR;
			crypto_buffer_t hash = new crypto_buffer_t();
			try
			{
				int computeStatus = computeKeyUnwrap(this.Key.ToArray(), (uint)this.Key.Count,
					this.Msg.ToArray(), (uint)this.Msg.Count,
					this.Iv.ToArray(), (uint)this.Iv.Count, out hash);
				result = (CRYPTO_STATUS)computeStatus;
			}
			catch (Exception e)
			{

			}

			if (result == CRYPTO_STATUS.CRYPTO_SUCCESS)
			{
				this.m_hash = new List<byte>(hash.buffer.Take((int)hash.bytes));
			}

			return result;
		}
	}
}
