/////*
// * @copyright 2016 Sean Connelly (@voidqk), http://syntheti.cc
// * @license MIT
// * @preserve Project Home: https://github.com/voidqk/polybooljs
// */

//using Engine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//using Point = System.Numerics.Vector<double>;

//namespace Engine.Experimental
//{
//    /// <summary>
//    /// The build log class.
//    /// used strictly for logging the processing of the algorithm... only useful if you intend on
//    /// looking under the covers (for pretty UI's or debugging)
//    /// </summary>
//    public class BuildLog
//    {
//        int nextSegmentId = 0;
//        int curVert = 0;//false;
//        StringBuilder list;

//        public bool push(object type, object data)
//        {
//            //         list.push({
//            //             type: type,
//            //data: data? JSON.parse(JSON.stringify(data)) : void 0

//            //     });
//            return true;
//        }
//        public int segmentId()
//        {
//            return nextSegmentId++;
//        }
//        public bool checkIntersection(object seg1, object seg2)
//        {
//            return push("check", (seg1: seg1, seg2: seg2));
//        }
//        public bool segmentChop(object seg, object end)
//        {
//            push("div_seg", (seg: seg, pt: end));
//            return push("chop", (seg: seg, pt: end));
//        }
//        public bool statusRemove(object seg)
//        {
//            return push("pop_seg", (seg: seg, nill: 0));
//        }
//        public bool segmentUpdate(object seg)
//        {
//            return push("seg_update", (seg: seg, nill: 0));
//        }
//        public bool segmentNew(object seg, object primary)
//        {
//            return push("new_seg", (seg: seg, primary: primary));
//        }
//        public bool segmentRemove(object seg)
//        {
//            return push("rem_seg", (seg: seg, nill: 0));
//        }
//        public bool tempStatus(object seg, object above, object below)
//        {
//            return push("temp_status", (seg: seg, above: above, below: below));
//        }
//        public bool rewind(object seg)
//        {
//            return push("rewind", (seg: seg, nill: 0));
//        }
//        public bool status(object seg, object above, object below)
//        {
//            return push("status", (seg: seg, above: above, below: below));
//        }
//        public bool vert(int x)
//        {
//            if (x == curVert)
//                return true;
//            curVert = x;
//            return push("vert", (x: x, nill: 0));
//        }
//        public bool log(object data)
//        {
//            //if (typeof data != string)
//            //    data = JSON.stringify(data, false, '  ');
//            return push("log", (txt: data, nill: 0));
//        }
//        public bool reset()
//        {
//            return push("reset", null);
//        }
//        public bool selected(object segs)
//        {
//            return push("selected", (segs: segs ));
//        }
//        public bool chainStart(object seg)
//        {
//            return push("chain_start", (seg: seg, nill: 0));
//        }
//        public bool chainRemoveHead(object index, object pt)
//        {
//            return push("chain_rem_head", (index: index, pt: pt));
//        }
//        public bool chainRemoveTail(object index, object pt)
//        {
//            return push("chain_rem_tail", (index: index, pt: pt));
//        }
//        public bool chainNew(object pt1, object pt2)
//        {
//            return push("chain_new", (pt1: pt1, pt2: pt2));
//        }
//        public bool chainMatch(object index)
//        {
//            return push("chain_match", (index: index, nill: 0));
//        }
//        public bool chainClose(object index)
//        {
//            return push("chain_close", (index: index, nill: 0));
//        }
//        public bool chainAddHead(object index, object pt)
//        {
//            return push("chain_add_head", (index: index, pt: pt));
//        }
//        public bool chainAddTail(object index, object pt)
//        {
//            return push("chain_add_tail", (index: index, pt: pt));
//        }
//        public bool chainConnect(object index1, object index2)
//        {
//            return push("chain_con", (index1: index1, index2: index2));
//        }
//        public bool chainReverse(object index)
//        {
//            return push("chain_rev", (index: index, nill: 0));
//        }
//        public bool chainJoin(object index1, object index2)
//        {
//            return push("chain_join", (index1: index1, index2: index2));
//        }
//        public bool done()
//        {
//            return push("done", null);
//        }
//    }

//    /// <summary>
//    /// The intersecter class.
//    /// this is the core work-horse
//    /// </summary>
//    public class Intersecter
//    {
//        /// <summary>
//        /// The self intersection.
//        /// selfIntersection is true/false depending on the phase of the overall algorithm
//        /// </summary>
//        bool selfIntersection;

//        /// <summary>
//        /// The eps.
//        /// </summary>
//        Epsilon eps;

//        /// <summary>
//        /// The build log.
//        /// </summary>
//        BuildLog buildLog;

//        /// <summary>
//        /// The segment new.
//        /// segment creation
//        /// </summary>
//        /// <param name="start">The start.</param>
//        /// <param name="end">The end.</param>
//        /// <returns>The <see cref="(int id, object start, object end, (object above, object below) myFill, object otherFill)"/>.</returns>
//        (int id, object start, object end, (object above, object below) myFill, object otherFill) segmentNew(object start, object end)
//        {
//            return (
//                id: buildLog?.segmentId() ?? -1,
//                start: start,
//                end: end,
//                myFill:
//                    (
//                        above: null, // is there fill above us?
//                        below: null  // is there fill below us?
//                    ),
//                otherFill: null
//            );
//        }

//        /// <summary>
//        /// The segment copy.
//        /// </summary>
//        /// <param name="start">The start.</param>
//        /// <param name="end">The end.</param>
//        /// <param name="seg">The seg.</param>
//        /// <returns>The <see cref="(int id, object start, object end, (object above, object below) myFill, object otherFill)"/>.</returns>
//        (int id, object start, object end, (object above, object below) myFill, object otherFill) segmentCopy(object start, object end, (int id, object start, object end, (object above, object below) myFill, object otherFill) seg)
//        {
//            return (
//                id: buildLog?.segmentId() ?? -1,
//                start: start,
//                end: end,
//                myFill:
//                    (
//                        above: seg.myFill.above,
//                        below: seg.myFill.below
//                    ),
//                    otherFill: null
//                );
//        }

//        //
//        // event logic
//        //

//        /// <summary>
//        /// The event root.
//        /// </summary>
//        LinkedList<object> event_root = new LinkedList<object>();

//        int eventCompare(bool p1_isStart, Point p1_1, double[] p1_2, bool p2_isStart, double[] p2_1, double[] p2_2)
//        {
//            // compare the selected points first
//            var comp = eps.pointsCompare(p1_1, p2_1);
//            if (comp != 0)
//                return comp;
//            // the selected points are the same

//            if (eps.pointsSame(p1_2, p2_2)) // if the non-selected points are the same too...
//                return 0; // then the segments are equal

//            if (p1_isStart != p2_isStart) // if one is a start and the other isn't...
//                return p1_isStart ? 1 : -1; // favor the one that isn't the start

//            // otherwise, we'll have to calculate which one is below the other manually
//            return eps.pointAboveOrOnLine(p1_2,
//                p2_isStart ? p2_1 : p2_2, // order matters
//                p2_isStart ? p2_2 : p2_1
//            ) ? 1 : -1;
//        }

//        void eventAdd(object ev, double[] other_pt)
//        {
//            event_root.AddBefore(ev, (here) =>
//            {
//                // should ev be inserted before here?
//                var comp = eventCompare(
//                    ev.isStart, ev.pt, other_pt,
//                    here.isStart, here.pt, here.other.pt
//                );
//                return comp < 0;
//            });
//        }

//        void eventAddSegmentStart(seg, primary)
//        {
//            var ev_start = LinkedList.node((
//                isStart: true,
//                pt: seg.start,
//                seg: seg,
//                primary: primary,
//                other: null,
//                status: null
//        ));
//            eventAdd(ev_start, seg.end);
//            return ev_start;
//        }

//        void eventAddSegmentEnd(ev_start, seg, primary)
//        {
//            var ev_end = LinkedList.node((
//                isStart: false,
//			pt: seg.end,
//			seg: seg,
//			primary: primary,
//			other: ev_start,
//			status: null
//        ));
//            ev_start.other = ev_end;
//            eventAdd(ev_end, ev_start.pt);
//        }

//        void eventAddSegment(seg, primary)
//        {
//            var ev_start = eventAddSegmentStart(seg, primary);
//            eventAddSegmentEnd(ev_start, seg, primary);
//            return ev_start;
//        }

//        void eventUpdateEnd(ev, end)
//        {
//            // slides an end backwards
//            //   (start)------------(end)    to:
//            //   (start)---(end)

//            if (buildLog)
//                buildLog.segmentChop(ev.seg, end);

//            ev.other.remove();
//            ev.seg.end = end;
//            ev.other.pt = end;
//            eventAdd(ev.other, ev.pt);
//        }

//        void eventDivide(ev, double[] pt)
//        {
//            var ns = segmentCopy(pt, ev.seg.end, ev.seg);
//            eventUpdateEnd(ev, pt);
//            return eventAddSegment(ns, ev.primary);
//        }

//        void calculate(primaryPolyInverted, secondaryPolyInverted)
//        {
//            // if selfIntersection is true then there is no secondary polygon, so that isn't used

//            //
//            // status logic
//            //

//            var status_root = LinkedList.create();

//            void statusCompare(ev1, ev2)
//            {
//                var a1 = ev1.seg.start;
//                var a2 = ev1.seg.end;
//                var b1 = ev2.seg.start;
//                var b2 = ev2.seg.end;

//                if (eps.pointsCollinear(a1, b1, b2))
//                {
//                    if (eps.pointsCollinear(a2, b1, b2))
//                        return 1;//eventCompare(true, a1, a2, true, b1, b2);
//                    return eps.pointAboveOrOnLine(a2, b1, b2) ? 1 : -1;
//                }
//                return eps.pointAboveOrOnLine(a1, b1, b2) ? 1 : -1;
//            }

//            void statusFindSurrounding(ev)
//            {
//                return status_root.findTransition(function(here){
//                    var comp = statusCompare(ev, here.ev);
//                    return comp > 0;
//                });
//        }

//        bool checkIntersection(ev1, ev2)
//        {
//            // returns the segment equal to ev1, or false if nothing equal

//            var seg1 = ev1.seg;
//            var seg2 = ev2.seg;
//            var a1 = seg1.start;
//            var a2 = seg1.end;
//            var b1 = seg2.start;
//            var b2 = seg2.end;

//            if (buildLog)
//                buildLog.checkIntersection(seg1, seg2);

//            var i = eps.linesIntersect(a1, a2, b1, b2);

//            if (i is null)
//            {
//                // segments are parallel or coincident

//                // if points aren't collinear, then the segments are parallel, so no intersections
//                if (!eps.pointsCollinear(a1, a2, b1))
//                    return false;
//                // otherwise, segments are on top of each other somehow (aka coincident)

//                if (eps.pointsSame(a1, b2) || eps.pointsSame(a2, b1))
//                    return false; // segments touch at endpoints... no intersection

//                var a1_equ_b1 = eps.pointsSame(a1, b1);
//                var a2_equ_b2 = eps.pointsSame(a2, b2);

//                if (a1_equ_b1 && a2_equ_b2)
//                    return ev2; // segments are exactly equal

//                var a1_between = !a1_equ_b1 && eps.pointBetween(a1, b1, b2);
//                var a2_between = !a2_equ_b2 && eps.pointBetween(a2, b1, b2);

//                // handy for debugging:
//                // buildLog.log({
//                //	a1_equ_b1: a1_equ_b1,
//                //	a2_equ_b2: a2_equ_b2,
//                //	a1_between: a1_between,
//                //	a2_between: a2_between
//                // });

//                if (a1_equ_b1)
//                {
//                    if (a2_between)
//                    {
//                        //  (a1)---(a2)
//                        //  (b1)----------(b2)
//                        eventDivide(ev2, a2);
//                    }
//                    else
//                    {
//                        //  (a1)----------(a2)
//                        //  (b1)---(b2)
//                        eventDivide(ev1, b2);
//                    }
//                    return ev2;
//                }
//                else if (a1_between)
//                {
//                    if (!a2_equ_b2)
//                    {
//                        // make a2 equal to b2
//                        if (a2_between)
//                        {
//                            //         (a1)---(a2)
//                            //  (b1)-----------------(b2)
//                            eventDivide(ev2, a2);
//                        }
//                        else
//                        {
//                            //         (a1)----------(a2)
//                            //  (b1)----------(b2)
//                            eventDivide(ev1, b2);
//                        }
//                    }

//                    //         (a1)---(a2)
//                    //  (b1)----------(b2)
//                    eventDivide(ev2, a1);
//                }
//            }
//            else
//            {
//                // otherwise, lines intersect at i.pt, which may or may not be between the endpoints

//                // is A divided between its endpoints? (exclusive)
//                if (i.alongA == 0)
//                {
//                    if (i.alongB == -1) // yes, at exactly b1
//                        eventDivide(ev1, b1);
//                    else if (i.alongB == 0) // yes, somewhere between B's endpoints
//                        eventDivide(ev1, i.pt);
//                    else if (i.alongB == 1) // yes, at exactly b2
//                        eventDivide(ev1, b2);
//                }

//                // is B divided between its endpoints? (exclusive)
//                if (i.alongB == 0)
//                {
//                    if (i.alongA == -1) // yes, at exactly a1
//                        eventDivide(ev2, a1);
//                    else if (i.alongA == 0) // yes, somewhere between A's endpoints (exclusive)
//                        eventDivide(ev2, i.pt);
//                    else if (i.alongA == 1) // yes, at exactly a2
//                        eventDivide(ev2, a2);
//                }
//            }
//            return false;
//        }

//        //
//        // main event loop
//        //
//        var segments = [];
//		while (!event_root.isEmpty()){
//			var ev = event_root.getHead();

//			if (buildLog)
//                buildLog.vert(ev.pt[0]);

//			if (ev.isStart){
//				if (buildLog)
//                    buildLog.segmentNew(ev.seg, ev.primary);

//        var surrounding = statusFindSurrounding(ev);
//        var above = surrounding.before ? surrounding.before.ev : null;
//        var below = surrounding.after ? surrounding.after.ev : null;

//				if (buildLog){
//					buildLog.tempStatus(
//                        ev.seg,
//                        above? above.seg : false,
//						below? below.seg : false
//					);
//    }

//    void checkBothIntersections()
//    void checkBothIntersections()
//    {
//        if (above)
//        {
//            var eve = checkIntersection(ev, above);
//            if (eve)
//                return eve;
//        }
//        if (below)
//            return checkIntersection(ev, below);
//        return false;
//    }

//    var eve = checkBothIntersections();
//				if (eve){
//					// ev and eve are equal
//					// we'll keep eve and throw away ev

//					// merge ev.seg's fill information into eve.seg

//					if (selfIntersection){
//						var toggle; // are we a toggling edge?
//						if (ev.seg.myFill.below is null)
//							toggle = true;
//						else
//							toggle = ev.seg.myFill.above != ev.seg.myFill.below;

//						// merge two segments that belong to the same polygon
//						// think of this as sandwiching two segments together, where `eve.seg` is
//						// the bottom -- this will cause the above fill flag to toggle
//						if (toggle)
//                            eve.seg.myFill.above = !eve.seg.myFill.above;
//}
//					else{
//						// merge two segments that belong to different polygons
//						// each segment has distinct knowledge, so no special logic is needed
//						// note that this can only happen once per segment in this phase, because we
//						// are guaranteed that all self-intersections are gone
//						eve.seg.otherFill = ev.seg.myFill;
//					}

//					if (buildLog)
//                        buildLog.segmentUpdate(eve.seg);

//ev.other.remove();
//					ev.remove();
//				}

//				if (event_root.getHead() != ev){
//					// something was inserted before us in the event queue, so loop back around and
//					// process it before continuing
//					if (buildLog)
//                        buildLog.rewind(ev.seg);
//					continue;
//				}

//				//
//				// calculate fill flags
//				//
//				if (selfIntersection){
//					var toggle; // are we a toggling edge?
//					if (ev.seg.myFill.below is null) // if we are a new segment...
//						toggle = true; // then we toggle
//					else // we are a segment that has previous knowledge from a division
//						toggle = ev.seg.myFill.above != ev.seg.myFill.below; // calculate toggle

//					// next, calculate whether we are filled below us
//					if (!below){ // if nothing is below us...
//						// we are filled below us if the polygon is inverted
//						ev.seg.myFill.below = primaryPolyInverted;
//					}
//					else{
//						// otherwise, we know the answer -- it's the same if whatever is below
//						// us is filled above it
//						ev.seg.myFill.below = below.seg.myFill.above;
//					}

//					// since now we know if we're filled below us, we can calculate whether
//					// we're filled above us by applying toggle to whatever is below us
//					if (toggle)
//                        ev.seg.myFill.above = !ev.seg.myFill.below;
//					else
//						ev.seg.myFill.above = ev.seg.myFill.below;
//				}
//				else{
//					// now we fill in any missing transition information, since we are all-knowing
//					// at this point

//					if (ev.seg.otherFill is null){
//						// if we don't have other information, then we need to figure out if we're
//						// inside the other polygon
//						var inside;
//						if (!below){
//							// if nothing is below us, then we're inside if the other polygon is
//							// inverted
//							inside =
//								ev.primary? secondaryPolyInverted : primaryPolyInverted;
//						}
//						else{ // otherwise, something is below us
//							// so copy the below segment's other polygon's above
//							if (ev.primary == below.primary)
//								inside = below.seg.otherFill.above;
//							else
//								inside = below.seg.myFill.above;
//						}
//						ev.seg.otherFill = {
//							above: inside,
//							below: inside
//						};
//					}
//				}

//				if (buildLog){
//					buildLog.status(
//                        ev.seg,
//                        above? above.seg : false,
//						below? below.seg : false
//					);
//				}

//				// insert the status and remember it for later removal
//				ev.other.status = surrounding.insert(LinkedList.node({ ev: ev }));
//			}
//			else{
//				var st = ev.status;

//				if (st is null){
//					throw new Error('PolyBool: Zero-length segment detected; your epsilon is ' +
//						'probably too small or too large');
//				}

//				// removing the status will create two new adjacent edges, so we'll need to check
//				// for those
//				if (status_root.exists(st.prev) && status_root.exists(st.next))
//					checkIntersection(st.prev.ev, st.next.ev);

//				if (buildLog)
//                    buildLog.statusRemove(st.ev.seg);

//// remove the status
//st.remove();

//				// if we've reached this point, we've calculated everything there is to know, so
//				// save the segment for reporting
//				if (!ev.primary){
//					// make sure `seg.myFill` actually points to the primary polygon though
//					var s = ev.seg.myFill;
//ev.seg.myFill = ev.seg.otherFill;
//					ev.seg.otherFill = s;
//				}
//				segments.push(ev.seg);
//			}

//			// remove the event and continue
//			event_root.getHead().remove();
//		}

//		if (buildLog)
//            buildLog.done();

//		return segments;
//	}

//	// return the appropriate API depending on what we're doing
//	if (!selfIntersection){
//		// performing combination of polygons, so only deal with already-processed segments
//		return {
//			calculate: function(segments1, inverted1, segments2, inverted2)
//{
//    // segmentsX come from the self-intersection API, or this API
//    // invertedX is whether we treat that list of segments as an inverted polygon or not
//    // returns segments that can be used for further operations
//    segments1.forEach(function(seg){
//        eventAddSegment(segmentCopy(seg.start, seg.end, seg), true);
//    });
//    segments2.forEach(function(seg){
//        eventAddSegment(segmentCopy(seg.start, seg.end, seg), false);
//    });
//    return calculate(inverted1, inverted2);
//}
//		};
//	}

//	// otherwise, performing self-intersection, so deal with regions
//	return {
//		addRegion: function(region)
//{
//    // regions are a list of points:
//    //  [ [0, 0], [100, 0], [50, 100] ]
//    // you can add multiple regions before running calculate
//    var pt1;
//    var pt2 = region[region.length - 1];
//    for (var i = 0; i < region.length; i++)
//    {
//        pt1 = pt2;
//        pt2 = region[i];

//        var forward = eps.pointsCompare(pt1, pt2);
//        if (forward == 0) // points are equal, so we have a zero-length segment
//            continue; // just skip it

//        eventAddSegment(
//            segmentNew(
//                forward < 0 ? pt1 : pt2,
//                forward < 0 ? pt2 : pt1
//            ),
//            true
//        );
//    }
//},
//		calculate: function(inverted)
//{
//    // is the polygon inverted?
//    // returns segments
//    return calculate(inverted, false);
//}
//	};
//}

//    /// <summary>
//    /// The epsilon class.
//    /// </summary>
//    public class Epsilon
//{
//    /// <summary>
//    /// The eps.
//    /// </summary>
//    public double eps = Maths.DoubleEpsilon;

//    /// <summary>
//    /// The point above or on line.
//    /// </summary>
//    /// <param name="pt">The pt.</param>
//    /// <param name="left">The left.</param>
//    /// <param name="right">The right.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointAboveOrOnLine(double[] pt, double[] left, double[] right)
//    {
//        var Ax = left[0];
//        var Ay = left[1];
//        var Bx = right[0];
//        var By = right[1];
//        var Cx = pt[0];
//        var Cy = pt[1];
//        return (Bx - Ax) * (Cy - Ay) - (By - Ay) * (Cx - Ax) >= -eps;
//    }

//    /// <summary>
//    /// The point between.
//    /// </summary>
//    /// <param name="p">The p.</param>
//    /// <param name="left">The left.</param>
//    /// <param name="right">The right.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointBetween(double[] p, double[] left, double[] right)
//    {
//        // p must be collinear with left->right
//        // returns false if p == left, p == right, or left == right
//        var d_py_ly = p[1] - left[1];
//        var d_rx_lx = right[0] - left[0];
//        var d_px_lx = p[0] - left[0];
//        var d_ry_ly = right[1] - left[1];

//        var dot = d_px_lx * d_rx_lx + d_py_ly * d_ry_ly;
//        // if `dot` is 0, then `p` == `left` or `left` == `right` (reject)
//        // if `dot` is less than 0, then `p` is to the left of `left` (reject)
//        if (dot < eps)
//            return false;

//        var sqlen = d_rx_lx * d_rx_lx + d_ry_ly * d_ry_ly;
//        // if `dot` > `sqlen`, then `p` is to the right of `right` (reject)
//        // therefore, if `dot - sqlen` is greater than 0, then `p` is to the right of `right` (reject)
//        if (dot - sqlen > -eps)
//            return false;

//        return true;
//    }

//    /// <summary>
//    /// The points same x.
//    /// </summary>
//    /// <param name="p1">The p1.</param>
//    /// <param name="p2">The p2.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointsSameX(double[] p1, double[] p2) => Math.Abs(p1[0] - p2[0]) < eps;

//    /// <summary>
//    /// The points same y.
//    /// </summary>
//    /// <param name="p1">The p1.</param>
//    /// <param name="p2">The p2.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointsSameY(double[] p1, double[] p2) => Math.Abs(p1[1] - p2[1]) < eps;

//    /// <summary>
//    /// The points same.
//    /// </summary>
//    /// <param name="p1">The p1.</param>
//    /// <param name="p2">The p2.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointsSame(double[] p1, double[] p2) => pointsSameX(p1, p2) && pointsSameY(p1, p2);

//    /// <summary>
//    /// The points compare.
//    /// </summary>
//    /// <param name="p1">The p1.</param>
//    /// <param name="p2">The p2.</param>
//    /// <returns>The <see cref="int"/>.</returns>
//    public int pointsCompare(double[] p1, double[] p2)
//    {
//        // returns -1 if p1 is smaller, 1 if p2 is smaller, 0 if equal
//        if (pointsSameX(p1, p2))
//            return pointsSameY(p1, p2) ? 0 : (p1[1] < p2[1] ? -1 : 1);
//        return p1[0] < p2[0] ? -1 : 1;
//    }

//    /// <summary>
//    /// The points collinear.
//    /// </summary>
//    /// <param name="pt1">The pt1.</param>
//    /// <param name="pt2">The pt2.</param>
//    /// <param name="pt3">The pt3.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointsCollinear(double[] pt1, double[] pt2, double[] pt3)
//    {
//        // does pt1->pt2->pt3 make a straight line?
//        // essentially this is just checking to see if the slope(pt1->pt2) == slope(pt2->pt3)
//        // if slopes are equal, then they must be collinear, because they share pt2
//        var dx1 = pt1[0] - pt2[0];
//        var dy1 = pt1[1] - pt2[1];
//        var dx2 = pt2[0] - pt3[0];
//        var dy2 = pt2[1] - pt3[1];
//        return Math.Abs(dx1 * dy2 - dx2 * dy1) < eps;
//    }

//    /// <summary>
//    /// The lines intersect.
//    /// </summary>
//    /// <param name="a0">The a0.</param>
//    /// <param name="a1">The a1.</param>
//    /// <param name="b0">The b0.</param>
//    /// <param name="b1">The b1.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public (double[] pt, double alongA, double alongB)? linesIntersect(double[] a0, double[] a1, double[] b0, double[] b1)
//    {
//        // returns null if the lines are coincident (e.g., parallel or on top of each other)
//        //
//        // returns an object if the lines intersect:
//        //   {
//        //     pt: [x, y],    where the intersection point is at
//        //     alongA: where intersection point is along A,
//        //     alongB: where intersection point is along B
//        //   }
//        //
//        //  alongA and alongB will each be one of: -2, -1, 0, 1, 2
//        //
//        //  with the following meaning:
//        //
//        //    -2   intersection point is before segment's first point
//        //    -1   intersection point is directly on segment's first point
//        //     0   intersection point is between segment's first and second points (exclusive)
//        //     1   intersection point is directly on segment's second point
//        //     2   intersection point is after segment's second point
//        var adx = a1[0] - a0[0];
//        var ady = a1[1] - a0[1];
//        var bdx = b1[0] - b0[0];
//        var bdy = b1[1] - b0[1];

//        var axb = adx * bdy - ady * bdx;
//        if (Math.Abs(axb) < eps)
//            return null; // lines are coincident

//        var dx = a0[0] - b0[0];
//        var dy = a0[1] - b0[1];

//        var A = (bdx * dy - bdy * dx) / axb;
//        var B = (adx * dy - ady * dx) / axb;

//        (double[] pt, double alongA, double alongB) ret = (new Double[] { a0[0] + A * adx, a0[1] + A * ady }, 0, 0);

//        // categorize where intersection point is along A and B

//        if (A <= -eps)
//            ret.alongA = -2;
//        else if (A < eps)

//            ret.alongA = -1;
//        else if (A - 1 <= -eps)
//            ret.alongA = 0;
//        else if (A - 1 < eps)
//            ret.alongA = 1;
//        else
//            ret.alongA = 2;

//        if (B <= -eps)
//            ret.alongB = -2;
//        else if (B < eps)

//            ret.alongB = -1;
//        else if (B - 1 <= -eps)
//            ret.alongB = 0;
//        else if (B - 1 < eps)
//            ret.alongB = 1;
//        else
//            ret.alongB = 2;

//        return ret;
//    }

//    /// <summary>
//    /// The point inside region.
//    /// </summary>
//    /// <param name="pt">The pt.</param>
//    /// <param name="region">The region.</param>
//    /// <returns>The <see cref="bool"/>.</returns>
//    public bool pointInsideRegion(double[] pt, double[][] region)
//    {
//        var x = pt[0];
//        var y = pt[1];
//        var last_x = region[region.Length - 1][0];
//        var last_y = region[region.Length - 1][1];
//        var inside = false;
//        for (var i = 0; i < region.Length; i++)
//        {
//            var curr_x = region[i][0];
//            var curr_y = region[i][1];

//            // if y is between curr_y and last_y, and
//            // x is to the right of the boundary created by the line
//            if ((curr_y - y > eps) != (last_y - y > eps) &&
//                (last_x - curr_x) * (y - curr_y) / (last_y - curr_y) + curr_x - x > eps)
//                inside = !inside;

//            last_x = curr_x;
//            last_y = curr_y;
//        }
//        return inside;
//    }
//}
//}

