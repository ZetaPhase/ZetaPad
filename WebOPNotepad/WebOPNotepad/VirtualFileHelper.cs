using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace WebOPNotepad
{
    static class VirtualFileHelper
    {
        public static void WriteAllText(IsolatedStorageFile storageScope, string fileName, string contents)
        {
            //Open the file stream (didn't know there was filemode, forgot)
            using (var fs = storageScope.OpenFile(fileName, FileMode.Create)) //This is a different kind of using
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(contents);
                }
            }
        }

        public static string ReadAllText(IsolatedStorageFile storageScope, string fileName)
        {
            string contents = null;
            //Open the file stream (didn't know there was filemode, forgot)
            using (var fs = storageScope.OpenFile(fileName, FileMode.Open)) //This is a different kind of using
            {
                using (var sw = new StreamReader(fs))
                {
                    contents = sw.ReadToEnd(); //we can't return contents because
                    //we have to dispose the stream and streamreader,
                   //if you don't understand, just dont wor trust me
                }
            }
            return contents;
        }
    }
}
