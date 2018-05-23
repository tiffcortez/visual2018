using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoVisual
{
    class Archivo
    {

        private string ruta;

        public string Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                ruta = value;
            }
        }
        public void Guardar(Grafo g)
        {
            var jsongrafo = JsonConvert.SerializeObject(g);
            System.IO.File.WriteAllText(ruta, jsongrafo);
            

           
            
        }

        public Grafo Abrir()
        {

            Grafo g = new Grafo();
            g = JsonConvert.DeserializeObject<Grafo>(System.IO.File.ReadAllText(ruta));

            

            return g;


        }
    }
}
