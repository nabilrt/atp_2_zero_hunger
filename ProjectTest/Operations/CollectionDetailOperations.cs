using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class CollectionDetailOperations
    {
        public static List<CollectionDetailModel> GetCollectionDetails(int id)
        {
            var db = new ZeroHungerDBEntities();
            List<CollectionDetailModel> collectionDetails = new List<CollectionDetailModel>();
            var collections=(from cl in db.CollectionDetails where cl.Collection_Id == id select cl).ToList();

            foreach (var collectionDetail in collections)
            {
                collectionDetails.Add(new CollectionDetailModel()
                {
                    Id=collectionDetail.Id,
                    Collection_Id=collectionDetail.Collection_Id,
                    Food_Name=collectionDetail.Food_Name
                });
            }

            return collectionDetails;

        }
    }
}