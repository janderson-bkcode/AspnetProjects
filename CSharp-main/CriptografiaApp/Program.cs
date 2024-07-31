using System;
using System.Security.Cryptography;
using System.Text;

public class RSAExample
{
    static void Main()
    {
        try
        {
            // Cria uma instância do provedor de serviços de criptografia RSA
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Gera um par de chaves pública/privada
                RSAParameters publicKey = rsa.ExportParameters(false);
                RSAParameters privateKey = rsa.ExportParameters(true);

                // Mensagem a ser criptografada
                string message = "Hello, World!";

                // Criptografa a mensagem usando a chave pública
                byte[] encryptedData = Encrypt(Encoding.UTF8.GetBytes(message), publicKey);

                // Descriptografa a mensagem usando a chave privada
                byte[] decryptedData = Decrypt(encryptedData, privateKey);

                // Exibe os resultados
                Console.WriteLine("Mensagem original: " + message);
                Console.WriteLine("Mensagem criptografada: " + Convert.ToBase64String(encryptedData));
                Console.WriteLine("Mensagem descriptografada: " + Encoding.UTF8.GetString(decryptedData));
            }
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("Erro de criptografia: " + e.Message);
        }
    }

    static byte[] Encrypt(byte[] data, RSAParameters publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            // Importa a chave pública fornecida para o provedor RSA
            rsa.ImportParameters(publicKey);

            // Criptografa os dados usando a chave pública
            return rsa.Encrypt(data, true);
        }
    }

    static byte[] Decrypt(byte[] data, RSAParameters privateKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            // Importa a chave privada fornecida para o provedor RSA
            rsa.ImportParameters(privateKey);

            // Descriptografa os dados usando a chave privada
            return rsa.Decrypt(data, true);
        }
    }
}
