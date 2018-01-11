using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HWA1.Controllers
{

    public class HadoopLogsController : Controller
    {
        private readonly IFileProvider _fileProvider;
        private IHostingEnvironment _Env;

        public HadoopLogsController(IFileProvider fileProvider, IHostingEnvironment envrnmt)
        {
            _fileProvider = fileProvider; _Env = envrnmt;
        }

        public IActionResult Index()
        {            

            try
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(str))
                {
                    throw new Exception("String Message Error");
                }
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
                //ReadError();
                //LogToHadoop();
            }


            var contents = _fileProvider.GetDirectoryContents("");
            return View(contents);
        }
        public void ErrorLogging(Exception ex)
        {
            var webRootInfo = _Env.WebRootPath;
            var contentRootPath = _Env.ContentRootPath;
            //System.IO.File.WriteAllText(file, "We are done");
            var strPath = System.IO.Path.Combine(contentRootPath, "Logs", "LocalLogFile.txt");
            //string strPath = "C:\\Users\\deepakk\\Desktop\\Log.txt";
            if (!System.IO.File.Exists(strPath))
            {
                System.IO.File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = System.IO.File.AppendText(strPath))
            {
                sw.WriteLine(DateTime.Now + " ===================================== Log Start =================================== ");
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine(DateTime.Now + " ===================================== Log End ===================================== ");

            }

            try
            {
                /*
                string srcFileName = strPath;
                string destFolderName = "/log_error";
                string destFileName = "Airlines_Errors.txt";


                Uri myUri = new Uri("http://localhost:50070/");
                string userName = "DeepakK";
                WebHDFSClient myClient = new WebHDFSClient(myUri, userName);


                myClient.DeleteDirectory(destFolderName, true).Wait();


                myClient.CreateDirectory(destFolderName).Wait();


                myClient.CreateFile(srcFileName, destFolderName + "/" + destFileName).Wait();
                */

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}