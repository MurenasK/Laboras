using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_3
{
    class HeroesContainer
    {
        private Heroes[] Heroes;
        private int Capacity { get; set; }

        public int Count { get; private set; }

        public string Race { get; set; }
        public string City { get; set; }
        public HeroesContainer()
        {
            this.Heroes = new Heroes[16];
        }

        public HeroesContainer(int capacity = 16)
        {
            this.Capacity = capacity;
            this.Heroes = new Heroes[capacity];
        }

        public Heroes Get(int index)
        {
            if (index < 0 || index >= Count)
            {
                Console.WriteLine("Nėra tokio indexo");
            }
            return Heroes[index];
        }

        public void Add(Heroes hero)
        {
            if (this.Count >= this.Capacity)
            {
                EnsureCapacity(this.Capacity * 2);
            }
            this.Heroes[this.Count++] = hero;
        }

        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Heroes[] temp = new Heroes[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.Heroes[i];
                }
                this.Capacity -= minimumCapacity;
                this.Heroes = temp;
            }
        }

        public void Sort()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    Heroes a = this.Heroes[i];
                    Heroes b = this.Heroes[i + 1];
                    if (a.CompareTo(b) < 0)
                    {
                        this.Heroes[i] = b;
                        this.Heroes[i + 1] = a;
                        flag = false;
                    }
                }
            }
        }

        public HeroesContainer(HeroesContainer container) : this()
        {
            for(int i = 0; i < container.Count; i++)
            {
                Heroes clonedHeroes = container.Get(i).Clone();
                this.Add(container.Get(i));
            }
        }

        public Heroes[] GetStrongestHeroes()
        {
            if (this.Count == 0) return new Heroes[0];

            HeroesContainer strongest = new HeroesContainer();
            strongest.Add(this.Get(0));

            for (int i = 1; i < this.Count; i++)
            {
                Heroes hero = this.Get(i);

                if (hero.IsStrongerThan(strongest.Get(0)))
                {
                    strongest = new HeroesContainer();
                    strongest.Add(hero);
                }
                else if (hero.IsEqualStrength(strongest.Get(0)))
                {
                    strongest.Add(hero);
                }
            }

            // Convert to array
            Heroes[] result = new Heroes[strongest.Count];
            for (int i = 0; i < strongest.Count; i++)
                result[i] = strongest.Get(i);

            return result;
        }


    }
}
