using System;
using static Assets.Scripts.Heroes.Heroe;
using static Assets.Scripts.GameDatas.NFTData;

namespace Assets.Scripts.GameDatas
{

    [Serializable]
    public class ItemData
    {
        public string assetId { get; set; }
        public AssetType itemType { get; set; }
        public Rarity rarity { get; set; }
        public Stats stats { get; set; }
        public Attributes attributes { get; set; }
        public string name { get; set; }
        public HeroeClassType heroeClassType { get; set; }
        public string lore { get; set; }
        public string url { get; set; }
    }
}
