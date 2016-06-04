//Pavel Gorelov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Application
{
    class Program
    {
        static void Main(string[] args)
        {

            
        }
		
		/// <summary>
        /// Opens the specified file for writing.
        /// </summary>
        /// <param name="folderName">The name of the folder containing the file.</param>
        /// <param name="fileName">The name of the file, including extension.</param>
        /// <returns>Stream used for writing the file's data.</returns>
        /// <remarks>If the specified file already exists, it will be overwritten.</remarks>
        private static Stream OpenFileForWrite(string folderName, string fileName)
        {
            Stream stream = null;

            Task task = new Task(
                            async () =>
                            {
                                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
                                StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                                stream = await file.OpenStreamForWriteAsync();
                            });
            task.Start();
            task.Wait();
			
            // Unity version
			//stream = new FileStream(Path.Combine(folderName, fileName), FileMode.Create, FileAccess.Write);

            return stream;
        
    }
}
