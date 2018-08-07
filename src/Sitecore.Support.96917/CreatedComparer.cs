using Sitecore.Data.Comparers;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
namespace Sitecore.Support.Data.Comparers
{
  public class CreatedComparer : Sitecore.Data.Comparers.CreatedComparer
  {
    public override IKey ExtractKey(Item item)
    {
      Assert.ArgumentNotNull(item, "item");
      KeyObj keyObj = new KeyObj();
      keyObj.Item = item;
      keyObj.Key = GetCreationDate(item);
      keyObj.Sortorder = item.Appearance.Sortorder;
      return keyObj;
    }

    private DateTime GetCreationDate(Item item)
    {
      DateTime dateTime = DateTime.MaxValue;
      Item[] versions = item.Versions.GetVersions(true);
      Item[] array = versions;
      foreach (Item item2 in array)
      {
        DateTime created = item2.Statistics.Created;
        if (created != DateTime.MinValue && created.CompareTo(dateTime) < 0)
        {
          dateTime = created;
        }
      }
      if (!(dateTime == DateTime.MaxValue))
      {
        return dateTime;
      }
      return DateTime.MinValue;
    }
  }

}