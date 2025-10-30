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
        private int Capacity;
        
        public int Count { get; private set; }
        /// <summary>
        /// Heroes container constructor
        /// </summary>
        /// <param name="allRegisters"></param>
        public HeroesContainer(HeroesContainer[] allRegisters)
        {
            this.Heroes = new Heroes[16];
        }
        /// <summary>
        /// Here we set the capacity of the container
        /// </summary>
        /// <param name="capacity"></param>
        public HeroesContainer(int capacity = 16)
        {
            this.Capacity = capacity;
            this.Heroes = new Heroes[capacity];
        }
        /// <summary>
        /// Get hero by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Heroes Get(int index)
        {
            if (index < 0 || index >= Count)
            {
                Console.WriteLine("Nėra tokio indexo");
            }
            return Heroes[index].Clone();
        }
        /// <summary>
        /// Adds hero to the container
        /// </summary>
        /// <param name="hero"></param>
        public void Add(Heroes hero)
        {
            if (this.Count >= this.Capacity)
            {
                EnsureCapacity(this.Capacity * 2);
            }
            this.Heroes[this.Count++] = hero.Clone();
        }
        /// <summary>
        /// Ensures capacity of the container
        /// </summary>
        /// <param name="minimumCapacity"></param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Heroes[] temp = new Heroes[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.Heroes[i];
                }
                this.Capacity = minimumCapacity;
                this.Heroes = temp;
            }
        }
        /// <summary>
        /// Išrikiuoja herojus pagal stiprumą (nuo stipriausio iki silpniausio)
        /// naudojant išrinkimo rūšiavimą.
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                int maxIndex = i; // laikome, kad dabartinis didžiausias yra i

                for (int j = i + 1; j < Count; j++)
                {
                    // Jei randame stipresnį herojų – išsaugome jo indeksą
                    if (Heroes[j].CompareTo(Heroes[maxIndex]) > 0)
                    {
                        maxIndex = j;
                    }
                }

                // Jei radome kitą didesnį – sukeičiam vietomis
                if (maxIndex != i)
                {
                    Heroes temp = Heroes[i];
                    Heroes[i] = Heroes[maxIndex];
                    Heroes[maxIndex] = temp;
                }
            }
        }

        /// <summary>
        /// Heroes container copy constructor
        /// </summary>
        /// <param name="container"></param>
        public HeroesContainer(HeroesContainer container) : this()
        {
            for(int i = 0; i < container.Count; i++)
            {
                Heroes clonedHeroes = container.Get(i).Clone();
                this.Add(container.Get(i));
            }
        }
        /// <summary>
        /// Clears the container
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                Heroes[i] = null; // release references
            Count = 0;
        }
        /// <summary>
        /// Puts hero at index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hero"></param>
        public void Put(int index, Heroes hero)
        {
            if (index < 0 || index >= Count)
            {
                Console.WriteLine("Nėra tokio indexo");
            }
            this.Heroes[index] = hero.Clone();
        }
        /// <summary>
        /// Inserts hero at index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hero"></param>
        public void Insert(int index, Heroes hero)
        {
            if (index < 0 || index > Count)
            {
                Console.WriteLine("Nėra tokio indexo");
            }
            if (this.Count + 1 > this.Capacity)
            {
                int newCapacity = Math.Max(this.Capacity * 2, this.Capacity + 1);
                EnsureCapacity(newCapacity);
            }
            if (this.Count >= this.Capacity)
            {
                EnsureCapacity(this.Capacity * 2);
            }
            for (int i = this.Count; i > index; i--)
            {
                this.Heroes[i] = this.Heroes[i - 1];
            }
            this.Heroes[index] = hero.Clone();
            this.Count++;
        }
        /// <summary>
        /// Removes hero at index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                Console.WriteLine("Nėra tokio indexo");
            }
            for (int i = index; i < this.Count - 1; i++)
            {
                this.Heroes[i] = this.Heroes[i + 1];
            }
            this.Heroes[this.Count - 1] = null;
            this.Count--;
        }
        /// <summary>
        /// Removes hero from container
        /// </summary>
        /// <param name="hero"></param>
        public void Remove(Heroes hero)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.Heroes[i].Equals(hero))
                {
                    RemoveAt(i);
                    return;
                }
            }
            Console.WriteLine("Herojus nerastas");
        }

    }
}
