using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Utils {
    public class ClickItem : MenuItemBase {
        public delegate void OnClickEvent(object sender, string text);
        public event OnClickEvent? onClick;

        public ClickItem(int id, string text, OnClickEvent handler) : base(id, text){
            this.onClick = handler;
        }

        public void FireOnClick() {
            onClick?.Invoke(this, Text);
        }
    }
}
