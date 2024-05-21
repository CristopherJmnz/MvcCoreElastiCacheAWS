using MvcCoreElastiCacheAWS.Models;
using System.Xml.Linq;

namespace MvcCoreElastiCacheAWS.Repository
{
    public class CochesRepository
    {
        private XDocument document;
        public CochesRepository()
        {
            string path = "MvcCoreElastiCacheAWS.Documents.coches.xml";
            Stream stream = this.GetType().Assembly
                .GetManifestResourceStream(path);
            this.document = XDocument.Load(stream);
        }

        public List<Coche> GetAllCoches()
        {
            var consulta = from datos in this.document.Descendants("coche")
                           select new Coche()
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Imagen = datos.Element("imagen").Value,
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value
                           };
            return consulta.ToList();
        }


        public Coche FindCoche(int id)
        {
            return this.GetAllCoches()
                .FirstOrDefault(x => x.IdCoche == id);
        }
    }
}
