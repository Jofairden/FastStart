using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FastStart
{
	/*
		(c) Jofairden 2017
		I should probably rewrite the entire thing sometime, this code sucks ass.
    */

	public class FastStart : Mod
	{
		public FastStart()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
			};
		}
	}

	public class FastStartPlayer : ModPlayer
	{
		public static List<Tuple<int, int>> EntryList = new List<Tuple<int, int>>();

		public static int[] PickaxeTool = new int[]
		{
			ItemID.IronPickaxe,
			ItemID.LeadPickaxe,
			ItemID.SilverPickaxe,
			ItemID.GoldPickaxe,
			ItemID.PlatinumPickaxe
		};

		public static int[] AxeTool = new int[]
		{
			ItemID.IronAxe,
			ItemID.LeadAxe,
			ItemID.SilverAxe,
			ItemID.GoldAxe,
			ItemID.PlatinumAxe
		};

		public static int[] HammerTool = new int[]
		{
			ItemID.IronHammer,
			ItemID.LeadHammer,
			ItemID.SilverHammer,
			ItemID.GoldHammer,
			ItemID.PlatinumHammer
		};

		public static int[] ArmorTop = new int[]
		{
			ItemID.IronHelmet,
			ItemID.LeadHelmet,
			ItemID.SilverHelmet,
			ItemID.GoldHelmet,
			ItemID.PlatinumHelmet
		};

		public static int[] ArmorMiddle = new int[]
		{
			ItemID.IronChainmail,
			ItemID.LeadChainmail,
			ItemID.SilverChainmail,
			ItemID.GoldChainmail,
			ItemID.PlatinumChainmail
		};

		public static int[] ArmorBottom = new int[]
		{
			ItemID.IronGreaves,
			ItemID.LeadGreaves,
			ItemID.SilverGreaves,
			ItemID.GoldGreaves,
			ItemID.PlatinumGreaves
		};

		public static int[] Weapon = new int[]
		{
			ItemID.IronShortsword,
			ItemID.LeadShortsword,
			ItemID.SilverShortsword,
			ItemID.GoldShortsword,
			ItemID.PlatinumShortsword
		};

		public static int[] WeaponExtra = new int[]
		{
			ItemID.EnchantedBoomerang,
			ItemID.BeeGun,
			ItemID.SlimeStaff,
			ItemID.HornetStaff,
			ItemID.Vilethorn
		};

		public static int[] Gift = new int[]
		{
			 ItemID.WinterCape,
			 ItemID.MysteriousCape,
			 ItemID.RedCape,
			 ItemID.CrimsonCloak,
			 ItemID.GingerBeard,
			 ItemID.AngelHalo,
			 ItemID.PartyBalloonAnimal,
			 ItemID.PartyBundleOfBalloonsAccessory,
		};

		public static int[] Accessory = new int[]
		{
			ItemID.ShinyRedBalloon,
			ItemID.CloudinaBottle,
			ItemID.GrapplingHook,
			ItemID.HermesBoots,
			ItemID.LuckyHorseshoe,
			ItemID.MoneyTrough
		};

		public static int[,] Misc = new int[,]
		{
			{ ItemID.LifeCrystal, 1, 4 },
			{ ItemID.ManaCrystal, 1, 4 },
			{ ItemID.WoodenArrow, 60, 151 },
			{ ItemID.Shuriken, 20, 56 },
			{ ItemID.ThrowingKnife, 20, 56 },
			{ ItemID.LesserHealingPotion, 1, 4 },
			{ ItemID.LesserManaPotion, 1, 4 },
			{ ItemID.RecallPotion, 1, 3 }
		};

		public static int[] Utilities = new int[]
		{
			ItemID.Torch,
			ItemID.Wood,
			ItemID.StoneBlock,
		};

		// ID, Chance 1/*, Stack
		public static int[,] Luck = new int[,]
		{
			{ ItemID.WandofSparking, 1, Main.rand.Next(51) }
		};

		public static List<int> PickaxeToolList;
		public static List<int> AxeToolList;
		public static List<int> HammerToolList;

		public static List<int> ArmorTopList;
		public static List<int> ArmorMiddleList;
		public static List<int> ArmorBottomList;

		public static List<int> WeaponList;
		public static List<int> WeaponExtraList;

		public static List<int> GiftList;
		public static List<int> AccessoryList;
		public static List<int> UtilitiesList;

		public static void SetUpLists()
		{
			PickaxeToolList = PickaxeTool.ToList();
			AxeToolList = AxeTool.ToList();
			HammerToolList = HammerTool.ToList();

			ArmorTopList = ArmorTop.ToList();
			ArmorMiddleList = ArmorMiddle.ToList();
			ArmorBottomList = ArmorBottom.ToList();

			WeaponList = Weapon.ToList();
			WeaponExtraList = WeaponExtra.ToList();

			GiftList = Gift.ToList();
			AccessoryList = Accessory.ToList();
			UtilitiesList = Utilities.ToList();
		}

		public static void AddToEntries(int entry, int stack)
		{
			EntryList.Add(new Tuple<int, int>(entry, stack));
		}

		// Add all items which were added  to the entry list, the user's inventory item list
		public static void AddAllEntries(ref IList<Item> items)
		{
			foreach (Tuple<int, int> tuple in EntryList)
			{
				Item entryItem = new Item();
				entryItem.SetDefaults(tuple.Item1);
				entryItem.stack = tuple.Item2;
				items.Add(entryItem);
			}
		}

		public enum EntryType
		{
			Tools,
			Armors,
			Weapons,
			Gifts,
			Accessories
		}

		// You get a platinum pickaxe?
		// Can't get platinum axe/hammer
		// Get a copper top? Can't get copper middle/bottom
		// And so on..
		// Makes our inventory truly random
		public static void RemoveEntries(EntryType entryType, int entryIndex)
		{
			if (entryType == EntryType.Tools)
			{
				PickaxeToolList.RemoveAt(entryIndex);
				AxeToolList.RemoveAt(entryIndex);
				HammerToolList.RemoveAt(entryIndex);
			}
			else if (entryType == EntryType.Armors)
			{
				ArmorTopList.RemoveAt(entryIndex);
				ArmorMiddleList.RemoveAt(entryIndex);
				ArmorBottomList.RemoveAt(entryIndex);
			}
			else if (entryType == EntryType.Weapons)
			{
				WeaponList.RemoveAt(entryIndex);
				WeaponExtraList.RemoveAt(entryIndex);
			}
			else if (entryType == EntryType.Gifts)
			{
				GiftList.RemoveAt(entryIndex);
			}
			else if (entryType == EntryType.Accessories)
			{
				AccessoryList.RemoveAt(entryIndex);
			}
		}

		public override void SetupStartInventory(IList<Item> items)
		{
			try
			{
				items.Clear(); // Remove default vanilla items
				SetUpLists(); // Setup (sort of ctor)

				// Grab random
				// Pickaxe
				var entryIndex = Main.rand.Next(PickaxeToolList.Count); // Select a random index from the list
				AddToEntries(PickaxeToolList.ElementAtOrDefault(entryIndex), 1); // Add to the selected entry to the entry list
				RemoveEntries(EntryType.Tools, entryIndex); // Remove from other entries

				//Axe
				entryIndex = Main.rand.Next(AxeToolList.Count);
				AddToEntries(AxeToolList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Tools, entryIndex);

				//Hammer
				entryIndex = Main.rand.Next(HammerToolList.Count);
				AddToEntries(HammerToolList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Tools, entryIndex);

				//ArmorTop
				entryIndex = Main.rand.Next(ArmorTopList.Count);
				AddToEntries(ArmorTopList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Armors, entryIndex);

				//ArmorMiddle
				entryIndex = Main.rand.Next(ArmorMiddleList.Count);
				AddToEntries(ArmorMiddleList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Armors, entryIndex);

				//ArmorBottom
				entryIndex = Main.rand.Next(ArmorBottomList.Count);
				AddToEntries(ArmorBottomList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Armors, entryIndex);

				//Weapon
				entryIndex = Main.rand.Next(WeaponList.Count);
				AddToEntries(WeaponList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Weapons, entryIndex);

				//Weapon
				entryIndex = Main.rand.Next(WeaponExtraList.Count);
				AddToEntries(WeaponExtraList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Weapons, entryIndex);

				//Gift
				entryIndex = Main.rand.Next(GiftList.Count);
				AddToEntries(GiftList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Gifts, entryIndex);
				entryIndex = Main.rand.Next(GiftList.Count);
				AddToEntries(GiftList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Gifts, entryIndex);

				//Accessory
				entryIndex = Main.rand.Next(AccessoryList.Count);
				AddToEntries(AccessoryList.ElementAtOrDefault(entryIndex), 1);
				RemoveEntries(EntryType.Accessories, entryIndex);

				// Misc
				#region misc
				int previousStack = 0;
				int currentStack = 0;

				for (int i = 0; i < Misc.GetUpperBound(0) - 1; i++)
				{
					entryIndex = i;
					bool addItem = true;
					currentStack = Main.rand.Next(Misc[entryIndex, 1], Misc[entryIndex, 2]);

					switch (i)
					{
						case 1:
						case 4:
						case 6:
							addItem = currentStack - previousStack > 0;
							break;
					}

					if (addItem)
					{
						AddToEntries(Misc[entryIndex, 0], currentStack);
					}

					previousStack = currentStack;
				}

				AddToEntries(Misc[Misc.GetUpperBound(0), 0], Main.rand.Next(Misc[Misc.GetUpperBound(0), 1], Misc[Misc.GetUpperBound(0), 2]));
				#endregion

				// Utilities
				UtilitiesList.ForEach(x => AddToEntries(x, Main.rand.Next(20, 56)));

				// Luck
				for (int i = 0; i < Luck.GetUpperBound(0); i++)
				{
					if (Main.rand.Next(Luck[i, 2]) == 0)
					{
						AddToEntries(Luck[i, 0], Luck[i, 1]);
					}
				}

				// Finalize
				AddAllEntries(ref items);
				EntryList.Clear();
			}
			catch (Exception e)
			{
				ErrorLogger.Log(e.ToString());
			}

		}
	}

}
