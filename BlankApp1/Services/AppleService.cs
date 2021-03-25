using BlankApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Services
{
    public interface IAppleService
    {
        List<AppleModel> GetApples();
        bool DeleteApple(int appleID);
        AppleModel SaveApple(AppleModel apple);
    }
    class AppleService : IAppleService
    {
        private List<AppleModel> _appleList;

        public AppleService()
        {
            this._appleList = new List<AppleModel>();
        }

        public bool DeleteApple(int appleID)
        {
            int index = this._appleList.FindIndex(apple => apple.ID == appleID);
            if (index >= 0)
            {
                this._appleList.RemoveAt(index);
            }
            return index >= 0;
        }

        public List<AppleModel> GetApples()
        {
            return this._appleList;
        }

        public AppleModel SaveApple(AppleModel apple)
        {
            if (!apple.ID.HasValue)
            {
                int lastID = _appleList.Aggregate(0, (max, current) => { return current.ID.Value > max ? current.ID.Value : max; });
                apple.ID = lastID + 1;
                this._appleList.Add(apple);
            }
            else
            {
                AppleModel theApple = this._appleList.Single(cur => cur.ID == apple.ID);
                theApple.Name = apple.Name;
                theApple.Colour = apple.Colour;
            }
            return apple;
        }
    }
}
