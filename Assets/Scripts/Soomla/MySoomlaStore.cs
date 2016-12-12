using Soomla.Store;

public class MySoomlaStore : IStoreAssets
{

    #region IStoreAssets implementation

    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { GOLD_COINS };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] {/* TEEN_MOVES_GOOD, FIVE_LIVES_GOOD, NO_ADS_GOOD, ONE_LIVES_GOOD, THREE_LIVES_GOOD */};
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { COINS_50_PACK, COINS_100_PACK, COINS_300_PACK, COINS_500_PACK, COINS_1000_PACK, COINS_7000_PACK };
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] { };
    }

    #endregion

    /// <summary>
    /// Coins
    /// </summary>
    public const string GOLD_COINS_PACK = "gold_coins_pack";
    public static VirtualCurrency GOLD_COINS = new VirtualCurrency("Gold coins", "", GOLD_COINS_PACK);

#if TEST_PURCHANSE
	public const string COINS_50_PACK_MARKET_ID = "android.test.purchased";
	public const string COINS_100_PACK_MARKET_ID = "android.test.purchased";
	public const string COINS_300_PACK_MARKET_ID = "android.test.purchased";
	public const string COINS_500_PACK_MARKET_ID = "android.test.purchased";
	public const string COINS_1000_PACK_MARKET_ID = "android.test.purchased";
	public const string COINS_7000_PACK_MARKET_ID = "android.test.purchased";
#else
    public const string COINS_50_PACK_MARKET_ID = "50_coins";
    public const string COINS_100_PACK_MARKET_ID = "100_coins";
    public const string COINS_300_PACK_MARKET_ID = "300_coins";
    public const string COINS_500_PACK_MARKET_ID = "500_coins";
    public const string COINS_1000_PACK_MARKET_ID = "1000_coins";
    public const string COINS_7000_PACK_MARKET_ID = "7000_coins";
#endif

    /// <summary>
    /// Purchases IDs
    /// </summary>
    public const string COINS_50 = "50_coins";
    public const string COINS_100 = "100_coins";
    public const string COINS_300 = "300_coins";
    public const string COINS_500 = "500_coins";
    public const string COINS_1000 = "1000_coins";
    public const string COINS_7000 = "7000_coins";

    /// <summary>
    /// 50 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_50_PACK = new VirtualCurrencyPack(
        "50 coins", 
        "A pack of 50 coins", 
        COINS_50, 50, 
        GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_50_PACK_MARKET_ID, 0.99)
        );

    /// <summary>
    /// 100 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_100_PACK = new VirtualCurrencyPack(
        "100 coins", 
        "A pack of 100 coins", 
        COINS_100, 100, GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_100_PACK_MARKET_ID, 1.99)
        );

    /// <summary>
    /// 300 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_300_PACK = new VirtualCurrencyPack(
        "300 coins", 
        "A pack of 300 coins", 
        COINS_300, 300, 
        GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_300_PACK_MARKET_ID, 3.99)
        );

    /// <summary>
    /// 500 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_500_PACK = new VirtualCurrencyPack(
        "500 coins", 
        "A pack of 500 coins", 
        COINS_500, 500, 
        GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_500_PACK_MARKET_ID, 5.99)
        );

    /// <summary>
    /// 1000 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_1000_PACK = new VirtualCurrencyPack(
        "1000 coins", 
        "A pack of 1000 coins", 
        COINS_1000, 1000, 
        GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_1000_PACK_MARKET_ID, 17.99)
        );

    /// <summary>
    /// 7000 Coins Pack
    /// </summary>
    VirtualCurrencyPack COINS_7000_PACK = new VirtualCurrencyPack(
        "7000 coins", 
        "A pack of 2000 coins",
        COINS_7000, 7000, 
        GOLD_COINS_PACK, 
        new PurchaseWithMarket(COINS_7000_PACK_MARKET_ID, 109.99)
        );
}
