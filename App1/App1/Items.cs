using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Items
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool done { get; set; }

        public Items(int Id, string title, string description, bool done)
        {
            this.Id = Id;
            this.title = title;
            this.description = description;
            this.done = done;
        }
    }
}
