using System.Collections.Generic;
using UnityEngine;

public class ContentLoader
{
    private static List<CardModel> CardModelsList = new List<CardModel>();
    
    public static List<CardModel> GetCardList() => CardModelsList;

    public static void GetListFromFile()
    {
        int imageId = 0;
        foreach (var image in Resources.LoadAll("images", typeof(Sprite)))
        {
            CardModelsList.Add(new CardModel()
            {
                Id = imageId,
                Name = image.name,
                Image = Resources.Load<Sprite>("images/" + image.name)
            });
            
            imageId++;
        }
    }
}
