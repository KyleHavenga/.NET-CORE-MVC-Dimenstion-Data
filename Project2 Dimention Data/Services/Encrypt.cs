using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Dimention_Data.Services
{
    public class Encrypt
    {
		public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV) //Decrypts session token
		{
			string plaintext = null;
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key; aesAlg.IV = IV;
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream MemoryStreamEncrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream CryptoStreamEncrypt = new CryptoStream(MemoryStreamEncrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(CryptoStreamEncrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			return plaintext;
		}


		public byte[] EncryptStringToBytes_Aes(string Text, byte[] Key, byte[] IV) // Encrypts session token
		{
			byte[] encryptedArray;
			using (Aes Values = Aes.Create())
			{
				Values.Key = Key; Values.IV = IV;
				ICryptoTransform encryptor = Values.CreateEncryptor(Values.Key, Values.IV);
				using (MemoryStream MemoryStreamEncrypt = new MemoryStream())
				{
					using (CryptoStream CryptoStreamEncrypt = new CryptoStream(MemoryStreamEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter StreamWriterEncrypt = new StreamWriter(CryptoStreamEncrypt)) { StreamWriterEncrypt.Write(Text); }
						encryptedArray = MemoryStreamEncrypt.ToArray();
					}
				}
			}
			return encryptedArray;
		}

		public string PassWordHashing(string value)
		{ //Hashes the encryption key 
			var sb = new StringBuilder();
			using (var hash = SHA256.Create())
			{
				var encrypt = Encoding.UTF8;
				var encryptionResult = hash.ComputeHash(encrypt.GetBytes(value));
				foreach (var item in encryptionResult) sb.Append(item.ToString("x2"));
			}
			return sb.ToString();
		}
	}
}
