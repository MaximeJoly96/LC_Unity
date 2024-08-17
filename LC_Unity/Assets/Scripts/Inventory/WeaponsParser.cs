﻿using System;
using System.Xml;
using System.Linq;
using Logging;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

namespace Inventory
{
    public class WeaponsParser : ItemParser
    {
        public static List<Weapon> ParseWeapons(TextAsset file)
        {
            List<Weapon> weapons = new List<Weapon>();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(file.text);

                XmlNodeList itemNodes = xmlDocument.SelectSingleNode("Items").SelectNodes("Item");
                foreach (XmlNode itemNode in itemNodes)
                {
                    Weapon weapon = ParseNodeIntoWeapon(itemNode);
                    weapons.Add(weapon);
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse weapons file. Reason: " + e.Message);
            }

            return weapons;
        }

        private static Weapon ParseNodeIntoWeapon(XmlNode itemNode)
        {
            int id = int.Parse(itemNode.SelectSingleNode("Id").InnerText);
            string name = itemNode.SelectSingleNode("Name").InnerText;
            string description = itemNode.SelectSingleNode("Description").InnerText;
            int icon = int.Parse(itemNode.SelectSingleNode("Icon").InnerText);
            int price = int.Parse(itemNode.SelectSingleNode("Price").InnerText);
            int animation = int.Parse(itemNode.SelectSingleNode("Animation").InnerText);
            int enchantSlots = int.Parse(itemNode.SelectSingleNode("EnchantmentSlots").InnerText);
            WeaponType type = (WeaponType)Enum.Parse(typeof(WeaponType), itemNode.SelectSingleNode("Type").InnerText);

            Weapon weapon = new Weapon(id, name, description, icon, price, ItemCategory.Weapon, animation, enchantSlots, type);

            weapon.Rank = int.Parse(itemNode.SelectSingleNode("Rank").InnerText);
            XmlNode recipeNode = itemNode.SelectSingleNode("Recipe");
            if(recipeNode != null)
                weapon.Recipe = ParseRecipeFromNode(recipeNode);

            XmlNode effectsNode = itemNode.SelectSingleNode("Effects");
            if(effectsNode != null)
                weapon.AddEffects(ParseEffectsFromNode(effectsNode));

            XmlNode projSpeedNode = itemNode.SelectSingleNode("ProjectileSpeed");
            if(projSpeedNode != null)
                weapon.ProjectileSpeed = float.Parse(projSpeedNode.InnerText, CultureInfo.InvariantCulture);

            XmlNode rangeNode = itemNode.SelectSingleNode("Range");
            if (rangeNode != null)
                weapon.Range = int.Parse(rangeNode.InnerText);

            return weapon;            
        }
    }
}