using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbService {
    public class TextSave {
        string path;

        public TextSave(string fileName) {
            path = Environment.CurrentDirectory + "/" + fileName;
        }

        public void Save(string text, bool append = false) {
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
