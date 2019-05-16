using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace testblob_0516
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //アカウントキーはアクセスキーの管理より確認
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storageblob0513;AccountKey=k1+MjmZyTmfKKD2ViDMSCpEarDlxuVZ/0Hmf/0nBaH2Z0Ao11KGZwxTLwDGuR8R7PaOWaBYPAGs2LLYg5PMhiQ==");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("testcontainer");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("test.txt");
 
            //アップロード
            using (var fileStream = System.IO.File.OpenRead("D:\validate\test1.txt"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            
            //ダウンロード
            blockBlob.DownloadToFile("/validate", System.IO.FileMode.CreateNew);

            */


            //storageAccountの作成（接続情報の定義）
            //アカウントネームやキー情報はAzureポータルから確認できる。
            var accountName = "storageblob0513";
            var accessKey = "25Zo5ak7IQp69XUi3874LL2ibSrsfj9sjYZOG5R5QZJvGuuxQvIbPEL89wb38Q3cOVQRaGgsQWTXABXkXDSiaQ==";
            var credential = new StorageCredentials(accountName, accessKey);
            var storageAccount = new CloudStorageAccount(credential, true);

            //こういう書き方もできるし、Configから読み込むこともできる。
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=アカウントネーム;AccountKey=アクセスキー");

            ////////////////// ここまでは各Storageサービス共通 //////////////////////////////////

            //blob
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //container
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            //もし無かったら作る
            //container.CreateIfNotExists();

            //upload

            //アップロード後のファイル名を指定（無くてよい）
            CloudBlockBlob blockBlob_upload = container.GetBlockBlobReference("test1.txt");

            //アップロード処理
            //アップロードしたいローカルのファイルを指定
            using (var fileStream = System.IO.File.OpenRead(@"D:\validate\test1.txt"))
            {
                blockBlob_upload.UploadFromStream(fileStream);
            }

            //download

            //ダウンロードするファイル名を指定
            CloudBlockBlob blockBlob_download = container.GetBlockBlobReference("test1.txt");

            //ダウンロード処理
            //ダウンロード後のパスとファイル名を指定。
            blockBlob_download.DownloadToFile(@"D:\validate\test2.txt", System.IO.FileMode.CreateNew);

            //削除
            //blockBlob_download.Delete();

            Console.WriteLine("処理を完了しました。");

            Console.ReadLine();
        }
    }
}
