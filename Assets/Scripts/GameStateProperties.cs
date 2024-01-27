using System.Collections.Generic;

public static class GameStateProperties
{
    public const string StateGranHasBeer = "state_gran_has_beer";
    public const string StateFrogCooked = "state_frog_cooked";
    public const string StateWellWithoutFrog = "state_well_without_frog";

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
    public const string ItemStrawberries = "item_strawberries";

    public const string ItemFrog = "item_frog";
    public const string ItemPotion = "item_potion";
    public const string ItemFlour = "item_flour";

    public const string ItemBucket = "item_bucket";
    public const string ItemWater = "item_water";

    public static readonly List<string> AllProperties = new ()
    {
        StateGranHasBeer,
        StateFrogCooked,
        StateWellWithoutFrog,

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
        ItemStrawberries,

        ItemFrog,
        ItemPotion,
        ItemFlour,

        ItemBucket,
        ItemWater
    };
}
