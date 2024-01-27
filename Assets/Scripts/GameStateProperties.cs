using System.Collections.Generic;

public static class GameStateProperties
{
    public const string PersonWitchHappy = "person_witch_happy";
    public const string PersonMillerHappy = "person_miller_happy";
    public const string PersonInnkeeperHappy = "person_innkeeper_happy";
    public const string PersonGranHappy = "person_gran_happy";

    public const string StateWellWithoutFrog = "state_well_without_frog";
    public const string StateBushCut = "state_bush_cut";
    public const string StateWellEmpty = "state_well_empty";

    public const string PlaceMap = "place_map";
    public const string PlaceHome = "place_home";
    public const string PlaceWitch = "place_witch";
    public const string PlacePub = "place_pub";
    public const string PlaceStrawberryPatch = "place_strawberry_patch";
    public const string PlaceGran = "place_gran";
    public const string PlaceBucket = "place_bucket";
    public const string PlaceWell = "place_well";

    public const string ItemMap = "item_map";

    public const string ItemBeer = "item_beer";
    public const string ItemMoney = "item_money";
    public const string ItemScythe = "item_scythe";
    public const string ItemBush = "item_bush";
    public const string ItemStrawberries = "item_strawberries";

    public const string ItemFrog = "item_frog";
    public const string ItemPotion = "item_potion";
    public const string ItemFlour = "item_flour";

    public const string ItemLeaf = "item_leaf";
    public const string ItemBucket = "item_bucket";
    public const string ItemWater = "item_water";

    public const string ItemPie = "item_pie";

    public static readonly List<string> AllProperties = new ()
    {
        PersonWitchHappy,
        PersonMillerHappy,
        PersonInnkeeperHappy,
        PersonGranHappy,

        StateWellWithoutFrog,
        StateBushCut,

        PlaceMap,
        PlaceHome,
        PlaceBucket,
        PlaceGran,
        PlaceWell,
        PlaceWitch,
        PlaceStrawberryPatch,
        PlacePub,

        ItemMap,

        ItemBeer,
        ItemMoney,
        ItemScythe,
        ItemBush,
        ItemStrawberries,

        ItemFrog,
        ItemPotion,
        ItemFlour,

        ItemLeaf,
        ItemBucket,
        ItemWater,

        ItemPie
    };
}
