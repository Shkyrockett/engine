using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Queue
    {
        public Func<SweepEvent, SweepEvent, double> _comparator { get; private set; }
        public List<SweepEvent> data { get; private set; }
        public int length { get; private set; }

        /**
* @param {Number} a
* @param {Number} b
* @return {Number}
*/
        public double DEFAULT_COMPARATOR(double a, double b)
        {
            return a < b ? -1 : a > b ? 1 : 0;
        }

        /**
         * @param {Array.<*>=} data
         * @param {Function=}  comparator
         */
        public Queue(SweepEvent[] data, Func<SweepEvent, SweepEvent, double> comparator)
        {
            this._comparator = comparator;// || DEFAULT_COMPARATOR;
            this.data = new List<SweepEvent> ();
            this.length = 0;
            if (data != null)
            {
                for (int i = 0, len = data.Length; i < len; i++)
                {
                    this.push(data[i]);
                }
            }
        }

        /**
         * First element
         * @return {*}
         */
        public SweepEvent peek()
        {
            return this.data[0];
        }

        /**
         * @return {*}
         */
        public SweepEvent pop()
        {
            var data = this.data;
            var first = data[0];
            //var last = data.pop();
            var size = --this.length;

            if (size == 0)
            {
                return first;
            }

            //data[0] = last;
            var current = 0;
            var compare = this._comparator;

            while (current < size)
            {
                var largest = current;
                var left = (2 * current) + 1;
                var right = (2 * current) + 2;

                if (left < size && compare(data[left], data[largest]) > 0)
                {
                    largest = left;
                }

                if (right < size && compare(data[right], data[largest]) > 0)
                {
                    largest = right;
                }

                if (largest == current) break;

                this._swap(largest, current);
                current = largest;
            }

            return first;
        }

        /**
         * @param {*} element
         * @return {Number} new size
         */
        public int push(SweepEvent element)
        {
            //var size = this.length = this.data.push(element);
            //var current = size - 1;
            //var compare = this._comparator;
            //var data = this.data;

            //while (current > 0)
            //{
            //    var parent = (int)Math.Floor((current - 1d) / 2d);
            //    if (compare(data[current], data[parent]) > 0) break;
            //    this._swap(parent, current);
            //    current = parent;
            //}

            //return size;
            return 0;
        }

        /**
         * @return {Number}
         */
        public double size()
        {
            return this.length;
        }

        /**
         * @param {Function} fn
         * @param {*}        context
         */
        public void forEach(Func<double> fn, double context)
        {
            //this.data.ForEach(fn, context);
        }

        /**
         * @param {Number} a
         * @param {Number} b
         */
        public void _swap(int a, int b)
        {
            var temp = this.data[a];
            this.data[a] = this.data[b];
            this.data[b] = temp;
        }
    };
}
