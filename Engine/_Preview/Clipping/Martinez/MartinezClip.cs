// <copyright file="MartinezClip.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Alexander Milevski
// </copyright>
// <author id="w8r">Alexander Milevski</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>Ported from https://github.com/w8r/martinez </summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class MartinezClip
    {
        int contourId = 0;

        /**
         * Signed area of the triangle (p0, p1, p2)
         * @param  {Array.<Number>} p0
         * @param  {Array.<Number>} p1
         * @param  {Array.<Number>} p2
         * @return {Number}
         */
        public static double signedArea(Point2D p0, Point2D p1, Point2D p2)
        {
            return (p0.X - p2.X) * (p1.Y - p2.Y) - (p1.X - p2.X) * (p0.Y - p2.Y);
        }

        public static bool equals(Point2D p1, Point2D p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        /**
         * @param  {SweepEvent} le1
         * @param  {SweepEvent} le2
         * @return {Number}
         */
        public static double compareSegments(SweepEvent le1, SweepEvent le2)
        {
            if (le1 == le2) return 0;

            // Segments are not collinear
            if (signedArea(le1.point, le1.otherEvent.point, le2.point) != 0 ||
              signedArea(le1.point, le1.otherEvent.point, le2.otherEvent.point) != 0)
            {

                // If they share their left endpoint use the right endpoint to sort
                if (equals(le1.point, le2.point)) return le1.isBelow(le2.otherEvent.point) ? -1 : 1;

                // Different left endpoint: use the left endpoint to sort
                if (le1.point.X == le2.point.X) return le1.point.Y < le2.point.Y ? -1 : 1;

                // has the line segment associated to e1 been inserted
                // into S after the line segment associated to e2 ?
                if (compareEvents(le1, le2) == 1) return le2.isAbove(le1.point) ? -1 : 1;

                // The line segment associated to e2 has been inserted
                // into S after the line segment associated to e1
                return le1.isBelow(le2.point) ? -1 : 1;
            }

            if (le1.isSubject == le2.isSubject)
            { // same polygon
                if (equals(le1.point, le2.point))
                {
                    if (equals(le1.otherEvent.point, le2.otherEvent.point))
                    {
                        return 0;
                    }
                    else
                    {
                        return le1.contourId > le2.contourId ? 1 : -1;
                    }
                }
            }
            else
            { // Segments are collinear, but belong to separate polygons
                return le1.isSubject ? -1 : 1;
            }

            return compareEvents(le1, le2) == 1 ? 1 : -1;
        }

        /**
             * @param  {SweepEvent} e1
             * @param  {SweepEvent} e2
             * @return {Number}
             */
        public static double compareEvents(SweepEvent e1, SweepEvent e2)
        {
            var p1 = e1.point;
            var p2 = e2.point;

            // Different x-coordinate
            if (p1.X > p2.X) return 1;
            if (p1.X < p2.X) return -1;

            // Different points, but same x-coordinate
            // Event with lower y-coordinate is processed first
            if (p1.Y != p2.Y) return p1.Y > p2.Y ? 1 : -1;

            return specialCases(e1, e2, p1, p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double specialCases(SweepEvent e1, SweepEvent e2, Point2D p1, Point2D p2)
        {
            // Same coordinates, but one is a left endpoint and the other is
            // a right endpoint. The right endpoint is processed first
            if (e1.left != e2.left)
                return e1.left ? 1 : -1;

            // Same coordinates, both events
            // are left endpoints or right endpoints.
            // not collinear
            if (signedArea(p1, e1.otherEvent.point, e2.otherEvent.point) != 0)
            {
                // the event associate to the bottom segment is processed first
                return (!e1.isBelow(e2.otherEvent.point)) ? 1 : -1;
            }

            //uncomment this if you want to play with multi-polygons
            if (e1.isSubject == e2.isSubject)
            {
                if (MartinezClip.equals(e1.point, e2.point) && e1.contourId == e2.contourId)
                {
                    return 0;
                }
                else
                {
                    return e1.contourId > e2.contourId ? 1 : -1;
                }
            }

            return (!e1.isSubject && e2.isSubject) ? 1 : -1;
        }

        /**
         * @param  {<Array.<Number>} s1
         * @param  {<Array.<Number>} s2
         * @param  {Boolean}         isSubject
         * @param  {Queue}           eventQueue
         * @param  {Array.<Number>}  bbox
         */
        public void processSegment(Point2D s1, Point2D s2, bool isSubject, int depth, Queue eventQueue, ref Rectangle2D bbox)
        {
            // Possible degenerate condition.
            // if (equals(s1, s2)) return;

            var e1 = new SweepEvent(s1, false, null, isSubject);
            var e2 = new SweepEvent(s2, false, e1, isSubject);
            e1.otherEvent = e2;

            e1.contourId = e2.contourId = depth;

            if (compareEvents(e1, e2) > 0)
            {
                e2.left = true;
            }
            else
            {
                e1.left = true;
            }

            bbox.Left = Math.Min(bbox.Left, s1.X);
            bbox.Top = Math.Min(bbox.Top, s1.Y);
            bbox.Right = Math.Max(bbox.Right, s1.X);
            bbox.Bottom = Math.Max(bbox.Bottom, s1.Y);

            // Pushing it so the queue is sorted from left to right,
            // with object on the left having the highest priority.
            eventQueue.push(e1);
            eventQueue.push(e2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="isSubject"></param>
        /// <param name="depth"></param>
        /// <param name="queue"></param>
        /// <param name="bbox"></param>
        public void processPolygon(Point2D[] polygon, bool isSubject, int depth, Queue queue, ref Rectangle2D bbox)
        {
            int i, len;
            for (i = 0, len = polygon.Length - 1; i < len; i++)
            {
                processSegment(polygon[i], polygon[i + 1], isSubject, depth + 1, queue, ref bbox);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="isSubject"></param>
        /// <param name="depth"></param>
        /// <param name="queue"></param>
        /// <param name="bbox"></param>
        public void processPolygon(Point2D[][] polygon, bool isSubject, int depth, Queue queue, ref Rectangle2D bbox)
        {
            int i, len;
            for (i = 0, len = polygon.Length; i < len; i++)
            {
                contourId++;
                processPolygon(polygon[i], isSubject, contourId, queue, ref bbox);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="clipping"></param>
        /// <param name="sbbox"></param>
        /// <param name="cbbox"></param>
        /// <returns></returns>
        public Queue fillQueue(Point2D[] subject, Point2D[] clipping, ref Rectangle2D sbbox, ref Rectangle2D cbbox)
        {
            var eventQueue = new Queue(null, compareEvents);
            contourId = 0;

            processPolygon(subject, true, 0, eventQueue, ref sbbox);
            processPolygon(clipping, false, 0, eventQueue, ref cbbox);

            return eventQueue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="clipping"></param>
        /// <param name="sbbox"></param>
        /// <param name="cbbox"></param>
        /// <returns></returns>
        public Queue fillQueue(Point2D[][] subject, Point2D[][] clipping, ref Rectangle2D sbbox, ref Rectangle2D cbbox)
        {
            var eventQueue = new Queue(null, compareEvents);
            contourId = 0;

            processPolygon(subject, true, 0, eventQueue, ref sbbox);
            processPolygon(clipping, false, 0, eventQueue, ref cbbox);

            return eventQueue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <param name="prev"></param>
        /// <param name="sweepLine"></param>
        /// <param name="operation"></param>
        public void computeFields(SweepEvent @event, SweepEvent prev, SweepEvent sweepLine, ClipingOperations operation)
        {
            // compute inOut and otherInOut fields
            if (prev == null)
            {
                @event.inOut = false;
                @event.otherInOut = true;

                // previous line segment in sweepline belongs to the same polygon
            }
            else if (@event.isSubject == prev.isSubject)
            {
                @event.inOut = !prev.inOut;
                @event.otherInOut = prev.otherInOut;

                // previous line segment in sweepline belongs to the clipping polygon
            }
            else
            {
                @event.inOut = !prev.otherInOut;
                @event.otherInOut = prev.isVertical() ? !prev.inOut : prev.inOut;
            }

            // compute prevInResult field
            if (prev != null)
            {
                @event.prevInResult = (!inResult(prev, operation) || prev.isVertical()) ?
                   prev.prevInResult : prev;
            }
            // check if the line segment belongs to the Boolean operation
            @event.inResult = inResult(@event, operation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool inResult(SweepEvent @event, ClipingOperations operation)
        {
            switch (@event.type)
            {
                case EdgeTypes.Normal:
                    switch (operation)
                    {
                        case ClipingOperations.Intersection:
                            return !@event.otherInOut;
                        case ClipingOperations.Union:
                            return @event.otherInOut;
                        case ClipingOperations.Difference:
                            return (@event.isSubject && @event.otherInOut) || (!@event.isSubject && !@event.otherInOut);
                        case ClipingOperations.Xor:
                            return true;
                    }
                    break;
                case EdgeTypes.SameTransition:
                    return operation == ClipingOperations.Intersection || operation == ClipingOperations.Union;
                case EdgeTypes.DifferentTransition:
                    return operation == ClipingOperations.Difference;
                case EdgeTypes.NonContributing:
                    return false;
            }
            return false;
        }

        /**
         * @param  {SweepEvent} se1
         * @param  {SweepEvent} se2
         * @param  {Queue}      queue
         * @return {Number}
         */
        public int possibleIntersection(SweepEvent se1, SweepEvent se2, Queue queue)
        {
            // that disallows self-intersecting polygons,
            // did cost us half a day, so I'll leave it
            // out of respect
            // if (se1.isSubject === se2.isSubject) return;

            //var inter = intersection(
            //  se1.point, se1.otherEvent.point,
            //  se2.point, se2.otherEvent.point
            //);

            //var nintersections = inter ? inter.length : 0;
            //if (nintersections == 0) return 0; // no intersection

            //// the line segments intersect at an endpoint of both line segments
            //if ((nintersections == 1) &&
            //    (equals(se1.point, se2.point) ||
            //     equals(se1.otherEvent.point, se2.otherEvent.point)))
            //{
            //    return 0;
            //}

            //if (nintersections == 2 && se1.isSubject == se2.isSubject)
            //{
            //    if (se1.contourId == se2.contourId)
            //    {
            //        Console.WriteLine("Edges of the same polygon overlap: {0} {1} {2} {3}", se1.point, se1.otherEvent.point, se2.point, se2.otherEvent.point);
            //    }
            //    //throw new Error('Edges of the same polygon overlap');
            //    return 0;
            //}

            //// The line segments associated to se1 and se2 intersect
            //if (nintersections == 1)
            //{

            //    // if the intersection point is not an endpoint of se1
            //    if (!equals(se1.point, inter[0]) && !equals(se1.otherEvent.point, inter[0]))
            //    {
            //        divideSegment(se1, inter[0], queue);
            //    }

            //    // if the intersection point is not an endpoint of se2
            //    if (!equals(se2.point, inter[0]) && !equals(se2.otherEvent.point, inter[0]))
            //    {
            //        divideSegment(se2, inter[0], queue);
            //    }
            //    return 1;
            //}

            //// The line segments associated to se1 and se2 overlap
            //var events = new List<SweepEvent>();
            //var leftCoincide = false;
            //var rightCoincide = false;

            //if (equals(se1.point, se2.point))
            //{
            //    leftCoincide = true; // linked
            //}
            //else if (compareEvents(se1, se2) == 1)
            //{
            //    events.push(se2, se1);
            //}
            //else
            //{
            //    events.push(se1, se2);
            //}

            //if (equals(se1.otherEvent.point, se2.otherEvent.point))
            //{
            //    rightCoincide = true;
            //}
            //else if (compareEvents(se1.otherEvent, se2.otherEvent) == 1)
            //{
            //    events.push(se2.otherEvent, se1.otherEvent);
            //}
            //else
            //{
            //    events.push(se1.otherEvent, se2.otherEvent);
            //}

            //if ((leftCoincide && rightCoincide) || leftCoincide)
            //{
            //    // both line segments are equal or share the left endpoint
            //    se1.type = EdgeTypes.NonContributing;
            //    se2.type = (se1.inOut == se2.inOut) ?
            //      EdgeTypes.SameTransition :
            //      EdgeTypes.DifferentTransition;

            //    if (leftCoincide && !rightCoincide)
            //    {
            //        // honestly no idea, but changing events selection from [2, 1]
            //        // to [0, 1] fixes the overlapping self-intersecting polygons issue
            //        divideSegment(events[0].otherEvent, events[1].point, queue);
            //    }
            //    return 2;
            //}

            //// the line segments share the right endpoint
            //if (rightCoincide)
            //{
            //    divideSegment(events[0], events[1].point, queue);
            //    return 3;
            //}

            //// no line segment includes totally the other one
            //if (events[0] != events[3].otherEvent)
            //{
            //    divideSegment(events[0], events[1].point, queue);
            //    divideSegment(events[1], events[2].point, queue);
            //    return 3;
            //}

            //// one line segment includes the other one
            //divideSegment(events[0], events[1].point, queue);
            //divideSegment(events[3].otherEvent, events[2].point, queue);

            return 3;
        }

        /**
         * @param  {SweepEvent} se
         * @param  {Array.<Number>} p
         * @param  {Queue} queue
         * @return {Queue}
         */
        public Queue divideSegment(SweepEvent se, Point2D p, Queue queue)
        {
            var r = new SweepEvent(p, false, se, se.isSubject);
            var l = new SweepEvent(p, true, se.otherEvent, se.isSubject);

            if (equals(se.point, se.otherEvent.point))
            {
                Console.WriteLine("what is that? {0}", se);
            }

            r.contourId = l.contourId = se.contourId;

            // avoid a rounding error. The left event would be processed after the right event
            if (compareEvents(l, se.otherEvent) > 0)
            {
                se.otherEvent.left = true;
                l.left = false;
            }

            // avoid a rounding error. The left event would be processed after the right event
            // if (compareEvents(se, r) > 0) {}

            se.otherEvent.otherEvent = l;
            se.otherEvent = r;

            queue.push(l);
            queue.push(r);

            return queue;
        }


        public bool iteratorEquals(SweepEvent it1, SweepEvent it2)
        {
            return true;// it1._cursor == it2._cursor;
        }


        //public void _renderSweepLine(SweepEvent sweepLine, Point2D pos, SweepEvent @event)
        //{
        //    var map = window.map;
        //    if (!map) return;
        //    if (window.sws) window.sws.forEach(function(p) {
        //        map.removeLayer(p);
        //    });
        //    window.sws = [];
        //    sweepLine.each(function(e) {
        //        var poly = L.polyline([e.point.slice().reverse(), e.otherEvent.point.slice().reverse()], { color: 'green' }).addTo(map);
        //        window.sws.push(poly);
        //    });

        //    if (window.vt) map.removeLayer(window.vt);
        //    var v = pos.slice();
        //    var b = map.getBounds();
        //    window.vt = L.polyline([[b.getNorth(), v[0]], [b.getSouth(), v[0]]], { color: 'green', weight: 1}).addTo(map);

        //    if (window.ps) map.removeLayer(window.ps);
        //        window.ps = L.polyline([@event.point.slice().reverse(), @event.otherEvent.point.slice().reverse()], {color: 'black', weight: 9, opacity: 0.4}).addTo(map);
        //    debugger;
        //}


        public SweepEvent[] subdivideSegments(Queue eventQueue, Point2D[] subject, Point2D[] clipping, Rectangle2D sbbox, Rectangle2D cbbox, ClipingOperations operation)
        {
            //var sweepLine = null;// new Tree(compareSegments);
            var sortedEvents = new SweepEvent[eventQueue.length];

            var rightbound = Math.Min(sbbox.Right, cbbox.Right);

            SweepEvent prev, next;

            while (eventQueue.length > 0)
            {
                var @event = eventQueue.pop();
                //sortedEvents.push(@event);

                // optimization by bboxes for intersection and difference goes here
                if ((operation == ClipingOperations.Intersection && @event.point.X > rightbound) ||
                    (operation == ClipingOperations.Difference && @event.point.X > sbbox.Right))
                {
                    break;
                }

                //if (@event.left)
                //{
                //    sweepLine.insert(@event);
                //    // _renderSweepLine(sweepLine, event.point, event);

                //    next = sweepLine.findIter(@event);
                //    prev = sweepLine.findIter(@event);
                //    @event.iterator = sweepLine.findIter(@event);

                //    // Cannot get out of the tree what we just put there
                //    if (prev == null || next == null)
                //    {
                //        Console.WriteLine("brute");
                //        var iterators = findIterBrute(sweepLine);
                //        prev = iterators[0];
                //        next = iterators[1];
                //    }

                //    //if (prev.data() != sweepLine.min())
                //    //{
                //    //    prev.prev();
                //    //}
                //    //else
                //    //{
                //    //    prev = sweepLine.iterator(); //findIter(sweepLine.max());
                //    //    prev.prev();
                //    //    prev.next();
                //    //}
                //    //next.next();

                //    //computeFields(@event, prev.data(), sweepLine, operation);

                //    //if (next.data())
                //    //{
                //    //    if (possibleIntersection(@event, next.data(), eventQueue) == 2)
                //    //    {
                //    //        computeFields(@event, prev.data(), sweepLine, operation);
                //    //        computeFields(@event, next.data(), sweepLine, operation);
                //    //    }
                //    //}

                //    //if (prev.data())
                //    //{
                //    //    if (possibleIntersection(prev.data(), @event, eventQueue) == 2)
                //    //    {
                //    //        var prevprev = sweepLine.findIter(prev.data());
                //    //        if (prevprev.data() != sweepLine.min())
                //    //        {
                //    //            prevprev.prev();
                //    //        }
                //    //        else
                //    //        {
                //    //            prevprev = sweepLine.findIter(sweepLine.max());
                //    //            prevprev.next();
                //    //        }
                //    //        computeFields(prev.data(), prevprev.data(), sweepLine, operation);
                //    //        computeFields(@event, prev.data(), sweepLine, operation);
                //    //    }
                //    //}
                //}
                //else
                //{
                //    @event = @event.otherEvent;
                //    next = sweepLine.findIter(@event);
                //    prev = sweepLine.findIter(@event);

                //    // _renderSweepLine(sweepLine, event.otherEvent.point, event);

                //    if (!(prev && next)) continue;

                //    if (prev.data() != sweepLine.min())
                //    {
                //        prev.prev();
                //    }
                //    else
                //    {
                //        prev = sweepLine.iterator();
                //        prev.prev(); // sweepLine.findIter(sweepLine.max());
                //        prev.next();
                //    }
                //    next.next();
                //    sweepLine.remove(@event);

                //    //_renderSweepLine(sweepLine, event.otherEvent.point, event);

                //    if (next.data() && prev.data())
                //    {
                //        possibleIntersection(prev.data(), next.data(), eventQueue);
                //    }
                //}
            }
            return sortedEvents;
        }

        public SweepEvent[] findIterBrute(SweepEvent sweepLine, Queue q)
        {
            var prev = sweepLine.iterator;
            var next = sweepLine.iterator;
            var it = sweepLine.iterator;
            object data;
            //while ((data = it.next()) != null)
            //{
            //    prev.next();
            //    next.next();
            //    if (data == @event)
            //    {
            //        break;
            //    }
            //}
            return new[] { prev, next };
        }


        public void swap<T>(List<T> arr, int i, int n)
        {
            var temp = arr[i];
            arr[i] = arr[n];
            arr[n] = temp;
        }

        public Point2D changeOrientation(Point2D contour)
        {
            return new Point2D(contour.Y,contour.X);
        }

        public bool isArray<T>(T[] arr)
        {
            return true;
        }

        public bool isArray<T>(T[][] arr)
        {
            return true;
        }


        public Point2D[][] addHole(Point2D[][] contour, int idx)
        {
            contour[idx] = new Point2D[] { };
            return contour;
        }

        public Point2D[][] addHole(Point2D[] contour, int idx)
        {
            var ctr = new Point2D[idx][];
            ctr[0] = contour;
            ctr[idx] = new Point2D[] { };
            return ctr;
        }

        /**
         * @param  {Array.<SweepEvent>} sortedEvents
         * @return {Array.<SweepEvent>}
         */
        public SweepEvent[] orderEvents(SweepEvent[] sortedEvents)
        {
            SweepEvent @event;
            int i, len;
            var resultEvents = new List<SweepEvent>();
            for (i = 0, len = sortedEvents.Length; i < len; i++)
            {
                @event = sortedEvents[i];
                if ((@event.left && @event.inResult) ||
                  (!@event.left && @event.otherEvent.inResult))
                {
                    resultEvents.Add(@event);
                }
            }

            // Due to overlapping edges the resultEvents array can be not wholly sorted
            var sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (i = 0, len = resultEvents.Count; i < len; i++)
                {
                    if ((i + 1) < len &&
                      compareEvents(resultEvents[i], resultEvents[i + 1]) == 1)
                    {
                        swap(resultEvents, i, i + 1);
                        sorted = false;
                    }
                }
            }

            for (i = 0, len = resultEvents.Count; i < len; i++)
            {
                //resultEvents[i].pos = i;
            }

            for (i = 0, len = resultEvents.Count; i < len; i++)
            {
                if (!resultEvents[i].left)
                {
                    //var temp = resultEvents[i].pos;
                    //resultEvents[i].pos = resultEvents[i].otherEvent.pos;
                    //resultEvents[i].otherEvent.pos = temp;
                }
            }

            return resultEvents.ToArray();
        }

        /**
         * @param  {Array.<SweepEvent>} sortedEvents
         * @return {Array.<*>} polygons
         */
        public Point2D[] connectEdges(SweepEvent[] sortedEvents)
        {
            int i, len;
            var resultEvents = orderEvents(sortedEvents);

            // "false"-filled array
            var processed = new bool[resultEvents.Length];
            var result = new List<Point2D>();

            var depth = new int[resultEvents.Length];
            var holeOf = new int[resultEvents.Length];
            var isHole = new bool[resultEvents.Length];

            for (i = 0, len = resultEvents.Length; i < len; i++)
            {
                if (processed[i]) continue;

                var contour = new Point2D();
                result.Add(contour);

                var ringId = result.Count - 1;
                //depth.push(0);
                //holeOf.push(-1);


                if (resultEvents[i].prevInResult!=null)
                {
                    var lowerContourId = resultEvents[i].prevInResult.contourId;
                    if (!resultEvents[i].prevInResult.resultInOut)
                    {
                        addHole(new[] { result[lowerContourId] }, ringId);
                        holeOf[ringId] = lowerContourId;
                        depth[ringId] = depth[lowerContourId] + 1;
                        isHole[ringId] = true;
                    }
                    else if (isHole[lowerContourId])
                    {
                        addHole(new[] { result[holeOf[lowerContourId]] }, ringId);
                        holeOf[ringId] = holeOf[lowerContourId];
                        depth[ringId] = depth[lowerContourId];
                        isHole[ringId] = true;
                    }
                }

                var pos = i;
                var initial = resultEvents[i].point;
                //contour.push(initial);

                while (pos >= i)
                {
                    processed[pos] = true;

                    if (resultEvents[pos].left)
                    {
                        resultEvents[pos].resultInOut = false;
                        resultEvents[pos].contourId = ringId;
                    }
                    else
                    {
                        resultEvents[pos].otherEvent.resultInOut = true;
                        resultEvents[pos].otherEvent.contourId = ringId;
                    }

                    //pos = resultEvents[pos].pos;
                    processed[pos] = true;

                    //contour.push(resultEvents[pos].point);
                    pos = nextPos(pos, resultEvents, processed);
                }

                pos = pos == -1 ? i : pos;

                //processed[pos] = processed[resultEvents[pos].pos] = true;
                resultEvents[pos].otherEvent.resultInOut = true;
                resultEvents[pos].otherEvent.contourId = ringId;


                // depth is even
                ///* eslint-disable no-bitwise */
                //if (depth[ringId] & 1)
                //{
                //    changeOrientation(contour);
                //}
                ///* eslint-enable no-bitwise */
            }

            return result.ToArray();
        }

        /**
         * @param  {Number} pos
         * @param  {Array.<SweepEvent>} resultEvents
         * @param  {Array.<Boolean>}    processed
         * @return {Number}
         */
        public int nextPos(int pos, SweepEvent[] resultEvents, bool[] processed)
        {
            var newPos = pos + 1;
            var length = resultEvents.Length;
            while (newPos < length &&
                   equals(resultEvents[newPos].point, resultEvents[pos].point))
            {
                if (!processed[newPos])
                {
                    return newPos;
                }
                else
                {
                    newPos = newPos + 1;
                }
            }

            newPos = pos - 1;

            while (processed[newPos])
            {
                newPos = newPos - 1;
            }
            return newPos;
        }


        public Point2D[] trivialOperation(Point2D[] subject, Point2D[] clipping, ClipingOperations operation)
        {
            Point2D[] result = null;
            if (subject.Length * clipping.Length == 0)
            {
                if (operation == ClipingOperations.Intersection)
                {
                    result = null;
                }
                else if (operation == ClipingOperations.Difference)
                {
                    result = subject;
                }
                else if (operation == ClipingOperations.Union || operation == ClipingOperations.Xor)
                {
                    result = (subject.Length == 0) ? clipping : subject;
                }
            }
            return result;
        }


        public Point2D[] compareBBoxes(Point2D[] subject, Point2D[] clipping, Rectangle2D sbbox, Rectangle2D cbbox, ClipingOperations operation)
        {
            Point2D[] result = null;
            if (sbbox.Left > cbbox.Right ||
                cbbox.Left > sbbox.Right ||
                sbbox.Top > cbbox.Bottom ||
                cbbox.Top > sbbox.Bottom)
            {
                if (operation == ClipingOperations.Intersection)
                {
                    result = null;
                }
                else if (operation == ClipingOperations.Difference)
                {
                    result = subject;
                }
                else if (operation == ClipingOperations.Union || operation == ClipingOperations.Xor)
                {
                    result = subject.Concat(clipping).ToArray();
                }
            }
            return result;
        }


        public Point2D[] boolean(Point2D[] subject, Point2D[] clipping, ClipingOperations operation)
        {
            var trivial = trivialOperation(subject, clipping, operation);
            if (trivial != null)
            {
                return trivial == null ? null : trivial;
            }
            var sbbox = new Rectangle2D(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);
            var cbbox = new Rectangle2D(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);

            var eventQueue = fillQueue(subject, clipping, ref sbbox, ref cbbox);

            trivial = compareBBoxes(subject, clipping, sbbox, cbbox, operation);
            if (trivial != null)
            {
                return trivial == null ? null : trivial;
            }
            var sortedEvents = subdivideSegments(eventQueue, subject, clipping, sbbox, cbbox, operation);
            return connectEdges(sortedEvents);
        }
    }
}
