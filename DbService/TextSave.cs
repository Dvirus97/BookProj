using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbService {
    public class TextSave {
        public string path { get; }

        public TextSave(string fileName) {
            if (string.IsNullOrWhiteSpace(fileName)) {
                using (StreamWriter sw = new StreamWriter("LogError.txt")) {
                    sw.WriteLine("file name is in valid. can't start new instance.");
                }
                throw new ArgumentNullException("file name is null ");
            }
            else
                path = Environment.CurrentDirectory + "/" + fileName;
        }

        public void Log(string text, bool append = false) {
            using (StreamWriter sw = new StreamWriter(path, append)) {
                sw.WriteLine(text);
            }
        }

        public string Load() {
            if (!File.Exists(path)) {
                using (FileStream fs = new FileStream(path, FileMode.Create)) { }
            }
            using (StreamReader sr = new StreamReader(path)) {
                return sr.ReadToEnd();
            }
        }
    }
}
