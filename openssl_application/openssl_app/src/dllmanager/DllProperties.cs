using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace openssl_app.dllmanager
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct crypto_buffer_t
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
		public byte[] buffer;
		public uint bytes;
	}

	static class DllProperties
	{
#if PLATFORM_X86
        public const string DLL_PATH = "openssl.dll";
#else // PLATFORM_X86
		public const string DLL_PATH = "openssl-x64.dll";
#endif // PLATFORM_X86
	}

	public enum CRYPTO_STATUS
	{
		CRYPTO_SUCCESS = 0,
		CRYPTO_ERROR,
		CRYPTO_ABORT_ERROR,
		CRYPTO_NOT_INITIALIZE_ERROR,
		CRYPTO_ALG_ERROR,
		CRYPTO_BUFFER_OVERFLOW_ERROR,
		CRYPTO_BUFFER_MISMATCH_ERROR,
		CRYPTO_NULL_PTR_ERROR,
		CRYPTO_INTEGRITY_ERROR,
		CRYPTO_NOT_SUPPORTED
	}

	public enum AES_MODE
	{
		AES_DECRYPT,
		AES_ENCRYPT,
		AES_KEY_WRAP,
		AES_KEY_UNWRAP
	}

	public enum RSA_MODE
	{
		RSA_VERIFY,
		RSA_SIGN
	}

	public enum AES_TYPE
	{
		//ECB
		AES_ECB_128 = 0,
		AES_ECB_192,
		AES_ECB_256,
		//CBC
		AES_CBC_128,
		AES_CBC_192,
		AES_CBC_256,
		//CTR
		AES_CTR_128,
		AES_CTR_192,
		AES_CTR_256,
		//GCM
		AES_GCM_128,
		AES_GCM_192,
		AES_GCM_256,
		//XTS
		AES_XTS_128,
		AES_XTS_256,
	}

	public enum SHA_TYPE
	{
		SHA_224,
		SHA_256,
		SHA_384,
		SHA_512
	}

}
