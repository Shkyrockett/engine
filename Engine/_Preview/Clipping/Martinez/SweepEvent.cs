using Engine._Preview;
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
    public class SweepEvent
    {
        /**
         * Is left endpoint?
         * @type {Boolean}
         */
        public bool left { get; set; }

        /**
         * @type {Array.<Number>}
         */
        public Point2D point { get; set; }

        /**
         * Other edge reference
         * @type {SweepEvent}
         */
        public SweepEvent otherEvent { get; set; }

        /**
         * Belongs to source or clipping polygon
         * @type {Boolean}
         */
        public bool isSubject { get; set; }

        /**
         * Edge contribution type
         * @type {Number}
         */
        public EdgeTypes type { get; set; }

        /**
         * In-out transition for the sweepline crossing polygon
         * @type {Boolean}
         */
        public bool inOut { get; set; }

        /**
         * @type {Boolean}
         */
        public bool otherInOut { get; set; }

        /**
         * Previous event in result?
         * @type {SweepEvent}
         */
        public SweepEvent prevInResult { get; set; }

        /**
         * Does event belong to result?
         * @type {Boolean}
         */
        public bool inResult { get; set; }

        /**
         * @type {Boolean}
         */
        public bool resultInOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int contourId { get; set; }


        public SweepEvent iterator { get; internal set; }

        /**
        * Sweepline event
        *
        * @param {Array.<Number>}  point
        * @param {Boolean}         left
        * @param {SweepEvent=}     otherEvent
        * @param {Boolean}         isSubject
        * @param {Number}          edgeType
        */
        public SweepEvent(Point2D point, bool left, SweepEvent otherEvent, bool isSubject, EdgeTypes edgeType = EdgeTypes.Normal)
        {
            this.left = left;
            this.point = point;
            this.otherEvent = otherEvent;
            this.isSubject = isSubject;
            this.type = edgeType;

            this.inOut = false;
            this.otherInOut = false;
            this.prevInResult = null;
            this.inResult = false;


            // connection step
            this.resultInOut = false;
        }

        /**
         * @param  {Array.<Number>}  p
         * @return {Boolean}
         */
        public bool isBelow(Point2D p)
        {
            return this.left ?
              MartinezClip.signedArea(this.point, this.otherEvent.point, p) > 0 :
              MartinezClip.signedArea(this.otherEvent.point, this.point, p) > 0;
        }

        /**
         * @param  {Array.<Number>}  p
         * @return {Boolean}
         */
        public bool isAbove(Point2D p)
        {
            return !this.isBelow(p);
        }

        /**
         * @return {Boolean}
         */
        public bool isVertical()
        {
            return this.point.X == this.otherEvent.point.X;
        }
    }
}
