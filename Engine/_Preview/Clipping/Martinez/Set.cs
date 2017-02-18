using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    class Set
    {
        /**
         * @type {*}
         */
        public object head { get; private set; }

        /**
         * @type {*}
         */
        public object tail { get; private set; }

        /**
         * @type {Number}
         */
        public int length { get; private set; }

        /**
         * @type {Function}
         */
        public Func<double, double, double> _comparator { get; private set; }

        /**
        * Doubly linked sorted list.
        * If you want to use it with raw numbers - wrap them into objects, or
        * they're going to be replaced on insert and it's gonna break
        *
        * @example
        * var list = new Set([{data: 50}, {data: 120}, {data: 10}]);
        * list.head; // {data:10, next: {data: 50, ...}, prev: {data: 120, ...}}
        * list.tail; // {data:120, next: {data: 10, ...}, prev: {data: 50, ...}}
        * 
        * @param {Array.<*>=} data
        * @param {Function=}  comparator
        */
        public Set(List<object> data, Func<double, double, double> comparator)
        {
            this.head = null;
            this.tail = null;
            this.length = 0;
            this._comparator = comparator;// || DEFAULT_COMPARATOR;

            if (data != null)
            {
                var len = data.Count;
                for (var i = 0; i < len; i++)
                {
                    this.insert(data[i]);
                }
            }
        }

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
         * @param  {*} a
         * @param  {*} b
         * @return {*} Inserted node
         */
        public object insertBefore(object a, object b)
        {
            //if (b == this.head)
            //{
            //    a.prev = this.tail;
            //    this.head = a;
            //    this.tail.next = this.head;
            //}
            //else
            //{
            //    a.prev = b.prev;
            //    b.prev.next = a;
            //}
            //a.next = b;
            //return b.prev = a;
            return null;
        }

        /**
         * @param  {*} a
         * @param  {*} b
         * @return {*} inserted node
         */
        public object insertAfter(object a, object b)
        {
            //if (b == this.tail)
            //{
            //    a.next = this.head;
            //    this.tail = a;
            //    this.head.prev = this.tail;
            //}
            //else
            //{
            //    a.next = b.next;
            //    b.next.prev = a;
            //}
            //a.prev = b;
            //return b.next = a;
            return null;
        }

        /**
         * @param  {*} node
         * @return {*}
         */
        public object insert(object node)
        {
            //var current, next;

            //this.length++;

            //if (this.head == null)
            //{
            //    this.head = node;
            //    this.head.next = this.head.prev = this.tail = this.head;
            //    return node;
            //}

            //if (this._comparator(this.head, node) > 0)
            //{
            //    this.insertBefore(node, this.head);
            //}
            //else
            //{
            //    current = this.head;
            //    while (current != this.tail)
            //    {
            //        next = current.next;
            //        if (this._comparator(next, node) > 0) break;
            //        current = current.next;
            //    }
            //    this.insertAfter(node, current);
            //}

            //if (this._comparator(node, this.head) < 0) this.head = node;
            //if (this._comparator(node, this.tail) > 0) this.tail = node;

            return node;
        }

        /**
         * @param  {*} node
         * @return {*}
         */
        public object remove(object node)
        {
            var current = this.head;
            //while (current != node)
            //{
            //    current = current.next;
            //    if (current == this.head) return;
            //}

            //if (current == this.head)
            //{
            //    this.head = this.tail.next = current.next;
            //    this.head.prev = this.tail;
            //}
            //else
            //{
            //    current.prev.next = current.next;
            //}

            //this.length--;

            //if (current == this.tail)
            //{
            //    this.tail = this.head.prev = current.prev;
            //    this.tail.next = this.head;
            //    return this.head;
            //}
            //else
            //{
            //    current.next.prev = current.prev;
            //    return current.prev;
            //}
            return null;
        }

        /**
         * @param  {*} node
         * @return {*|Null}
         */
        public object find(object node)
        {
            //var current;
            //if (!this.head)
            //{
            //    // empty list
            //    return null;
            //}
            //else
            //{
            //    current = this.head;
            //    while (current.next != this.head)
            //    {
            //        if (current == node) return current;
            //        current = current.next;
            //    }
            //    return null;
            //}
            return null;
        }

        /**
         * @return {Array.<*>}
         */
        public object toArray()
        {
            //var arr = new[] { };
            //var current = this.head;
            //while (current != this.tail)
            //{
            //    arr.push(current);
            //    current = current.next;
            //}
            //return arr;
            return null;
        }
    }
}
