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
        /// Swaps heroes if they are out of order
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool SwapIfOutOfOrder(int index)
        {
            // bounds safety: index and index+1 must be valid positions within Count
            if (index < 0 || index + 1 >= this.Count) return false;

            Heroes a = this.Heroes[index];
            Heroes b = this.Heroes[index + 1];

            if (a == null || b == null) return false;

            // keep original ordering: if a < b (according to CompareTo) then swap
            if (a.CompareTo(b) < 0)
            {
                this.Heroes[index] = b;
                this.Heroes[index + 1] = a;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sorts the container using bubble sort
        /// </summary>
        public void Sort()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    if (SwapIfOutOfOrder(i))
                        flag = true;
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
