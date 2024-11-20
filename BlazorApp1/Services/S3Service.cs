using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Threading.Tasks;

public class S3Service
{
    private readonly IAmazonS3 _s3Client;
    private const string bucketName = "trailerspeliculas";

public S3Service()
{
    
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.USEast2
    };
    _s3Client = new AmazonS3Client(credentials, config);
}


    public async Task<string> UploadFileAsync(string filePath, string keyName)
    {
        try
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            // Configurar la solicitud de carga con el Content-Type adecuado
            var request = new TransferUtilityUploadRequest
            {
                FilePath = filePath,
                BucketName = bucketName,
                Key = keyName,
                ContentType = "video/mp4" // Establece el tipo de contenido como video/mp4
            };

            await fileTransferUtility.UploadAsync(request);
            Console.WriteLine("Archivo cargado exitosamente en S3 con la clave: " + keyName);
            return "Archivo cargado exitosamente.";
        }
        catch (AmazonS3Exception e)
        {
            return $"Error en el servidor. Mensaje:'{e.Message}' al subir el archivo";
        }
        catch (Exception e)
        {
            return $"Error desconocido. Mensaje:'{e.Message}' al subir el archivo";
        }
    }

    // Nueva función para obtener una URL pre-firmada
    public string GetPreSignedURL(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = fileName,
            Expires = DateTime.UtcNow.AddMinutes(30) // URL válida por 30 minutos
        };

        return _s3Client.GetPreSignedURL(request);
    }
}
