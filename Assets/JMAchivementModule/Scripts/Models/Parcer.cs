using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;

public class Parcer {
	static Parcer _instance = null;
	public static Parcer Instance(){
		if (_instance == null) {
			_instance = new Parcer ();
		}
		return _instance;
	}

	public JMAchivementPack[] LoadModels() {
		string path = "AchivementXml";
		TextAsset asset = Resources.Load<TextAsset>(path);
		if(asset == null)
			return null;
		
		string xml = asset.text;

		XmlDocument xDoc = new XmlDocument();
		xDoc.LoadXml(xml);
		// получим корневой элемент
		XmlElement xRoot = xDoc.DocumentElement;
		int count = xRoot.ChildNodes.Count;
		JMAchivementPack[] jmAchivementPacks = new JMAchivementPack[count];
		for (int i = 0; i < jmAchivementPacks.Length; i++) {
			jmAchivementPacks [i] = new JMAchivementPack ();
		}
		// обход всех узлов в корневом элементе
		int numberId = -1;
		foreach(XmlNode xnode in xRoot)
		{
			numberId++;
			if(xnode.Attributes.Count>0)
			{
				XmlNode attr = xnode.Attributes.GetNamedItem("id");
				if (attr != null) {
					string id = attr.Value;
					jmAchivementPacks[numberId].id = id;

				}
					
			}

			foreach(XmlNode packNode in xnode.ChildNodes)
			{
				if (packNode.Name == "names") {
					if (packNode.Attributes.Count > 0) {
						XmlNode attr = packNode.Attributes.GetNamedItem (JMAchivementSettings.jmAchivementSettings.GetLanguageParcer ());
						if (attr != null) {
							string name = attr.Value;
							jmAchivementPacks [numberId].name = name;
						}
					}
				} 
				else if (packNode.Name == "achivements") {
					int numberAchivement = -1;
					jmAchivementPacks [numberId].jmAchivements = new JMAchivement[packNode.ChildNodes.Count];
					for (int i = 0; i < packNode.ChildNodes.Count; i++) {
						jmAchivementPacks [numberId].jmAchivements [i] = new JMAchivement ();
					}
					foreach(XmlNode achivementNode in packNode.ChildNodes)
					{
						numberAchivement++;
						foreach (XmlNode childAchivementNode in achivementNode.ChildNodes) {
							if (childAchivementNode.Name == "description") {
								XmlNode attr = childAchivementNode.Attributes.GetNamedItem (JMAchivementSettings.jmAchivementSettings.GetLanguageParcer ());
								if (attr != null) {
									string description = attr.Value;
									jmAchivementPacks [numberId].jmAchivements [numberAchivement].description = description;
								}
							} else if (childAchivementNode.Name == "maxProgress") {
								string maxProgress = childAchivementNode.InnerText;
								jmAchivementPacks [numberId].jmAchivements [numberAchivement].maxProgress = (float)Convert.ToDouble (maxProgress);
							} 
							else if (childAchivementNode.Name == "honors") {
								int numberHonor = -1;
								jmAchivementPacks [numberId].jmAchivements [numberAchivement].honors = new JMHonor[childAchivementNode.ChildNodes.Count];
								for (int i = 0; i < childAchivementNode.ChildNodes.Count; i++) {
									jmAchivementPacks [numberId].jmAchivements [numberAchivement].honors[i] = new JMHonor ();
								}
								foreach (XmlNode honorNode in childAchivementNode) {
									numberHonor++;
									XmlNode attr = honorNode.Attributes.Item (0);
									if (attr != null) {
										string honorName = attr.Value;
										jmAchivementPacks [numberId].jmAchivements [numberAchivement].honors [numberHonor].type = JMAchivementSettings.jmAchivementSettings.GetHonorType (honorName);
									}
									attr = honorNode.Attributes.GetNamedItem("isBool");
									if (attr != null) {
										string honorBoolType = attr.Value;
										jmAchivementPacks [numberId].jmAchivements [numberAchivement].honors [numberHonor].isBoolType = Convert.ToBoolean(honorBoolType);
									}
									attr = honorNode.Attributes.GetNamedItem("count");
									if (attr != null) {
										string honorCount = attr.Value;
										jmAchivementPacks [numberId].jmAchivements [numberAchivement].honors [numberHonor].count = Convert.ToInt32(honorCount);
									}
								}
							}
						}
					}
				}	
			}
		}
			
		return jmAchivementPacks;
	}
}
