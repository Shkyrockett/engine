using System.Collections.Generic;
using static Engine.SegmentComparators;

namespace Engine
{
    /// <summary>
    /// The boolean op imp class.
    /// </summary>
    public class BooleanOpImp
    {
        /// <summary>
        /// The subject.
        /// </summary>
        private Polygon subject;

        /// <summary>
        /// The clipping.
        /// </summary>
        private Polygon clipping;

        /// <summary>
        /// The result.
        /// </summary>
        private Polygon result;

        /// <summary>
        /// The operation.
        /// </summary>
        private ClippingOperations operation;

        /// <summary>
        /// event queue (sorted events to be processed)
        /// </summary>
        private PriorityQueue<SweepEvent> eq = new PriorityQueue<SweepEvent>();

        /// <summary>
        /// segments intersecting the sweep line
        /// </summary>
        private SortedSet<SweepEvent> sl = new SortedSet<SweepEvent>();

        /// <summary>
        /// It holds the events generated during the computation of the boolean operation
        /// </summary>
        private LinkedList<SweepEvent> eventHolder = new LinkedList<SweepEvent>();

        /// <summary>
        /// to compare events
        /// </summary>
        private SweepEventComp sec = new SweepEventComp();

        /// <summary>
        /// The sorted events (readonly). Value: new LinkedList&lt;SweepEvent&gt;().
        /// </summary>
        private readonly LinkedList<SweepEvent> sortedEvents = new LinkedList<SweepEvent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanOpImp"/> class.
        /// </summary>
        /// <param name="subj">The subj.</param>
        /// <param name="clip">The clip.</param>
        /// <param name="result">The result.</param>
        /// <param name="op">The op.</param>
        public BooleanOpImp(Polygon subj, Polygon clip, Polygon result, ClippingOperations op)
        {
            subject = subj;
            clipping = clip;
            this.result = result;
            operation = op;
        }

        /// <summary>
        /// The run.
        /// </summary>
        public static void Run()
        {
            //// for optimizations 1 and 2
            //Rectangle2D subjectBB = subject.Bounds;

            //// for optimizations 1 and 2
            //Rectangle2D clippingBB = clipping.Bounds;

            //// for optimization 2
            //var MINMAXX = Min(subjectBB.Right, clippingBB.Right);

            //// trivial cases can be quickly resolved without sweeping the plane
            //if (TrivialOperation(subjectBB, clippingBB))
            //    return;
            //for (var i = 0; i < subject.Contours.Count; i++)
            //    for (var j = 0; j < subject.Contours[i].Count; j++)
            //        ProcessSegment(subject.Contours[i].Segment(j), PolygonRelations.Subject);
            //for (var i = 0; i < clipping.Contours.Count; i++)
            //    for (var j = 0; j < clipping.Contours[i].Count; j++)
            //        ProcessSegment(clipping.Contours[i].Segment(j), PolygonRelations.Clipping);

            //SortedSet<SweepEvent> it, prev, next;

            //while (!eq.IsEmpty)
            //{
            //    SweepEvent se = eq.Top();
            //    // optimization 2
            //    if ((operation == ClipingOperations.Intersection && se.Point.X > MINMAXX) ||
            //        (operation == ClipingOperations.Difference && se.Point.X > subjectBB.Right))
            //    {
            //        ConnectEdges();
            //        return;
            //    }
            //    sortedEvents.Add(se);
            //    eq.Pop();
            //    if (se.IsLeft)
            //    { // the line segment must be inserted into sl
            //        next = prev = se.posSL = it = sl.insert(se).first;
            //        (prev != sl.begin()) ? --prev : prev = sl.end();
            //        ++next;
            //        ComputeFields(se, prev);
            //        // Process a possible intersection between "se" and its next neighbor in sl
            //        if (next != sl.end())
            //        {
            //            if (possibleIntersection(se, next) == 2)
            //            {
            //                ComputeFields(se, prev);
            //                computeFields(next, it);
            //            }
            //        }
            //        // Process a possible intersection between "se" and its previous neighbor in sl
            //        if (prev != sl.end())
            //        {
            //            if (possibleIntersection(prev, se) == 2)
            //            {
            //                SortedSet<SweepEvent> prevprev = prev;
            //                (prevprev != sl.begin()) ? --prevprev : prevprev = sl.end();
            //                computeFields(prev, prevprev);
            //                ComputeFields(se, prev);
            //            }
            //        }
            //    }
            //    else
            //    {                                 // the line segment must be removed from sl
            //        se = se.OtherEvent;          // we work with the left event
            //        next = prev = it = se.posSL; // se.posSL; is equal than sl.find (se); but faster
            //        (prev != sl.begin()) ? --prev : prev = sl.end();
            //        ++next;
            //        // delete line segment associated to "se" from sl and check for intersection between the neighbors of "se" in sl
            //        sl.erase(it);
            //        if (next != sl.end() && prev != sl.end())
            //            possibleIntersection(prev, next);
            //    }
            //}
            //ConnectEdges();
        }

        /// <summary>
        /// The trivial operation.
        /// </summary>
        /// <param name="subjectBB">The subjectBB.</param>
        /// <param name="clippingBB">The clippingBB.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool TrivialOperation(Rectangle2D subjectBB, Rectangle2D clippingBB)
        {
            //// Test 1 for trivial result case
            //if (subject.ncontours() * clipping.ncontours() == 0)
            //{ // At least one of the polygons is empty
            //    if (operation == ClipingOperations.Difference )
            //        result = subject;
            //    if (operation == ClipingOperations.Union  || operation == ClipingOperations.Xor )
            //        result = (subject.ncontours() == 0) ? clipping : subject;
            //    return true;
            //}
            //// Test 2 for trivial result case
            //if (subjectBB.xmin() > clippingBB.xmax() || clippingBB.xmin() > subjectBB.xmax() ||
            //    subjectBB.ymin() > clippingBB.ymax() || clippingBB.ymin() > subjectBB.ymax())
            //{
            //    // the bounding boxes do not overlap
            //    if (operation == ClipingOperations.Difference )
            //        result = subject;
            //    if (operation == ClipingOperations.Union  || operation == ClipingOperations.Xor )
            //    {
            //        result = subject;
            //        result.Join(clipping);
            //    }
            //    return true;
            //}
            return false;
        }

        /// <summary>
        /// Process the segment.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="pt">The pt.</param>
        public static void ProcessSegment(LineSegment s, ClippingRelations pt)
        {
            //*	if (s.degenerate ()) // if the two edge endpoints are equal the segment is dicarded
            //    return;          // This can be done as preprocessing to avoid "polygons" with less than 3 edges */
            //SweepEvent e1 = storeSweepEvent(SweepEvent(true, s.source(), 0, pt));
            //SweepEvent e2 = storeSweepEvent(SweepEvent(true, s.target(), e1, pt));
            //e1.OtherEvent = e2;

            //if (s.min() == s.source())
            //{
            //    e2.left = false;
            //}
            //else
            //{
            //    e1.left = false;
            //}
            //eq.Push(e1);
            //eq.Push(e2);
        }

        /* @brief Store the SweepEvent e into the event holder, returning the address of e */
        private SweepEvent StoreSweepEvent(SweepEvent e)
        {
            eventHolder.AddLast(e);
            return eventHolder.First.Value;
        }

        /// <summary>
        /// The compute fields.
        /// </summary>
        /// <param name="le">The le.</param>
        /// <param name="prev">The prev.</param>
        public static void ComputeFields(SweepEvent le, SortedSet<SweepEvent> prev)
        {
            //// compute inOut and otherInOut fields
            //if (prev == sl.end())
            //{
            //    le.InOut = false;
            //    le.OtherInOut = true;
            //}
            //else if (le.BelongsTo == (prev).BelongsTo)
            //{ // previous line segment in sl belongs to the same polygon that "se" belongs to
            //    le.InOut = !(prev).InOut;
            //    le.OtherInOut = (prev).otherInOut;
            //}
            //else
            //{ // previous line segment in sl belongs to a different polygon that "se" belongs to
            //    le.InOut = !(prev).OtherInOut;
            //    le.OtherInOut = (prev).vertical() ? !(prev).InOut : (prev).InOut;
            //}
            //// compute prevInResult field
            //if (prev != sl.end())
            //    le.PrevInResult = (!inResult(prev) || (prev).Vertical()) ? (prev).PrevInResult : prev;
            //// check if the line segment belongs to the Boolean operation
            //le.InResult = InResult(le);
        }

        /// <summary>
        /// The in result.
        /// </summary>
        /// <param name="le">The le.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool InResult(SweepEvent le)
        {
            switch (le.Contribution)
            {
                case EdgeContributions.Normal:
                    switch (operation)
                    {
                        case ClippingOperations.Intersection:
                            return !le.OtherInOut;
                        case ClippingOperations.Union:
                            return le.OtherInOut;
                        case ClippingOperations.Difference:
                            return (le.BelongsTo == ClippingRelations.Subject && le.OtherInOut) || (le.BelongsTo == ClippingRelations.Clipping && !le.OtherInOut);
                        case ClippingOperations.Xor:
                            return true;
                    }
                    break;
                case EdgeContributions.SameTransition:
                    return operation == ClippingOperations.Intersection || operation == ClippingOperations.Union;
                case EdgeContributions.DifferentTransition:
                    return operation == ClippingOperations.Difference;
                case EdgeContributions.NonContributing:
                    return false;
            }
            return false; // just to avoid the compiler warning
        }

        /// <summary>
        /// The possible intersection.
        /// </summary>
        /// <param name="le1">The le1.</param>
        /// <param name="le2">The le2.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int PossibleIntersection(SweepEvent le1, SweepEvent le2)
        {
            ////	if (e1.pol == e2.pol) // you can uncomment these two lines if self-intersecting polygons are not allowed
            ////		return 0;

            //Point2D ip1 = Point2D.Empty, ip2 = Point2D.Empty; // intersection points
            //int nintersections;

            //if (!(nintersections = FindIntersection(le1.Segment(), le2.Segment(), ip1, ip2)))
            //    return 0; // no intersection

            //if ((nintersections == 1) && ((le1.Point == le2.Point) || (le1.OtherEvent.Point == le2.OtherEvent.Point)))
            //    return 0; // the line segments intersect at an endpoint of both line segments

            //if (nintersections == 2 && le1.BelongsTo == le2.BelongsTo)
            //{
            //    //std::cerr << "Sorry, edges of the same polygon overlap\n";
            //    //exit(1); // the line segments overlap, but they belong to the same polygon
            //    return;
            //}

            //// The line segments associated to le1 and le2 intersect
            //if (nintersections == 1)
            //{
            //    if (le1.Point != ip1 && le1.OtherEvent.Point != ip1) // if the intersection point is not an endpoint of le1.segment ()
            //        DivideSegment(le1, ip1);
            //    if (le2.Point != ip1 && le2.OtherEvent.Point != ip1) // if the intersection point is not an endpoint of le2.segment ()
            //        DivideSegment(le2, ip1);
            //    return 1;
            //}
            //// The line segments associated to le1 and le2 overlap
            //List<SweepEvent> sortedEvents;
            //if (le1.Point == le2.Point)
            //{
            //    sortedEvents.Add(0);
            //}
            //else if (sec(le1, le2))
            //{
            //    sortedEvents.Add(le2);
            //    sortedEvents.Add(le1);
            //}
            //else
            //{
            //    sortedEvents.Add(le1);
            //    sortedEvents.Add(le2);
            //}
            //if (le1.OtherEvent.Point == le2.OtherEvent.Point)
            //{
            //    sortedEvents.Add(0);
            //}
            //else if (sec(le1.OtherEvent, le2.OtherEvent))
            //{
            //    sortedEvents.Add(le2.OtherEvent);
            //    sortedEvents.Add(le1.OtherEvent);
            //}
            //else
            //{
            //    sortedEvents.Add(le1.OtherEvent);
            //    sortedEvents.Add(le2.OtherEvent);
            //}

            //if ((sortedEvents.Count == 2) || (sortedEvents.Count == 3 && sortedEvents[2]))
            //{
            //    // both line segments are equal or share the left endpoint
            //    le1.Contribution = EdgeContributions.NonContributing;
            //    le2.Contribution = (le1.InOut == le2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition;
            //    if (sortedEvents.Count == 3)
            //        DivideSegment(sortedEvents[2].OtherEvent, sortedEvents[1].Point);
            //    return 2;
            //}
            //if (sortedEvents.Count == 3)
            //{ // the line segments share the right endpoint
            //    DivideSegment(sortedEvents[0], sortedEvents[1].Point);
            //    return 3;
            //}
            //if (sortedEvents[0] != sortedEvents[3].OtherEvent)
            //{ // no line segment includes totally the other one
            //    DivideSegment(sortedEvents[0], sortedEvents[1].Point);
            //    DivideSegment(sortedEvents[1], sortedEvents[2].Point);
            //    return 3;
            //}
            //// one line segment includes the other one
            //DivideSegment(sortedEvents[0], sortedEvents[1].Point);
            //DivideSegment(sortedEvents[3].OtherEvent, sortedEvents[2].Point);
            return 3;
        }

        /// <summary>
        /// The divide segment.
        /// </summary>
        /// <param name="le">The le.</param>
        /// <param name="p">The p.</param>
        public void DivideSegment(SweepEvent le, Point2D p)
        {
            //	std::cout << "YES. INTERSECTION" << std::endl;
            // "Right event" of the "left line segment" resulting from dividing le.segment ()
            var r = StoreSweepEvent(new SweepEvent(false, p, le, le.BelongsTo /*, le.type*/));
            // "Left event" of the "right line segment" resulting from dividing le.segment ()
            var l = StoreSweepEvent(new SweepEvent(true, p, le.OtherEvent, le.BelongsTo /*, le.other.type*/));
            if (SweepEventComp(l, le.OtherEvent) != 0)
            {
                // avoid a rounding error. The left event would be processed after the right event
                //std::cout << "Oops" << std::endl;
                le.OtherEvent.IsLeft = true;
                l.IsLeft = false;
            }
            //if (SweepEventComp(le, r))
            //{
            //    // avoid a rounding error. The left event would be processed after the right event
            //    //std::cout << "Oops2" << std::endl;
            //}
            le.OtherEvent.OtherEvent = l;
            le.OtherEvent = r;
            eq.Push(l);
            eq.Push(r);
        }

        /// <summary>
        /// The connect edges.
        /// </summary>
        public static void ConnectEdges()
        {
            //// copy the events in the result polygon to resultEvents array
            //var resultEvents = new List<SweepEvent>(sortedEvents.Count);

            //for (var it = sortedEvents.First; it != sortedEvents.Last; it++)
            //    if (((it).Previous.Value && (it).Value) || (!(it).Previous.Value && it.OtherEvent.inResult))
            //        resultEvents.Add(it.Value);

            //// Due to overlapping edges the resultEvents array can be not wholly sorted
            //var sorted = false;
            //while (!sorted)
            //{
            //    sorted = true;
            //    for (var i = 0; i < resultEvents.Count; ++i)
            //    {
            //        if (i + 1 < resultEvents.Count && sec(resultEvents[i], resultEvents[i + 1]))
            //        {
            //            Maths.Swap(resultEvents[i], resultEvents[i + 1]);
            //            sorted = false;
            //        }
            //    }
            //}

            //for (var i = 0; i < resultEvents.Count; ++i)
            //{
            //    resultEvents[i].Pos = i;
            //    if (!resultEvents[i].left)
            //        Maths.Swap(resultEvents[i].Pos, resultEvents[i].OtherEvent.pos);
            //}

            //List<bool> processed(resultEvents.Count, false);
            //var depth = new List<int>();
            //var holeOf = new List<int>();
            //for (var i = 0; i < resultEvents.Count; i++)
            //{
            //    if (processed[i])
            //        continue;
            //    result.Add(Contour());
            //    Contour  contour = result.back();
            //    int contourId = result.ncontours() - 1;
            //    depth.Add(0);
            //    holeOf.Add(-1);
            //    if (resultEvents[i].PrevInResult)
            //    {
            //        var lowerContourId = resultEvents[i].PrevInResult.ContourId;
            //        if (!resultEvents[i].PrevInResult.ResultInOut)
            //        {
            //            result[lowerContourId].AddHole(contourId);
            //            holeOf[contourId] = lowerContourId;
            //            depth[contourId] = depth[lowerContourId] + 1;
            //            contour.SetExternal(false);
            //        }
            //        else if (!result[lowerContourId].External())
            //        {
            //            result[holeOf[lowerContourId]].AddHole(contourId);
            //            holeOf[contourId] = holeOf[lowerContourId];
            //            depth[contourId] = depth[lowerContourId];
            //            contour.SetExternal(false);
            //        }
            //    }
            //    var pos = i;
            //    Point2D initial = resultEvents[i].Point;
            //    contour.Add(initial);
            //    while (resultEvents[pos].OtherEvent.Point != initial)
            //    {
            //        processed[pos] = true;
            //        if (resultEvents[pos].left)
            //        {
            //            resultEvents[pos].ResultInOut = false;
            //            resultEvents[pos].contourId = contourId;
            //        }
            //        else
            //        {
            //            resultEvents[pos].OtherEvent.ResultInOut = true;
            //            resultEvents[pos].OtherEvent.ContourId = contourId;
            //        }
            //        processed[pos = resultEvents[pos].pos] = true;
            //        contour.add(resultEvents[pos].Point);
            //        pos = nextPos(pos, resultEvents, processed);
            //    }
            //    processed[pos] = processed[resultEvents[pos].pos] = true;
            //    resultEvents[pos].OtherEvent.ResultInOut = true;
            //    resultEvents[pos].OtherEvent.contourId = contourId;
            //    if (depth[contourId] & 1)
            //        contour.changeOrientation();
            //}
        }

        /// <summary>
        /// The next pos.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="resultEvents">The resultEvents.</param>
        /// <param name="processed">The processed.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int NextPos(int pos, List<SweepEvent> resultEvents, List<bool> processed)
        {
            var newPos = pos + 1;
            while (newPos < resultEvents.Count && resultEvents[newPos].Point == resultEvents[pos].Point)
            {
                if (!processed[newPos])
                    return newPos;
                else
                    ++newPos;
            }
            newPos = pos - 1;
            while (processed[newPos])
                --newPos;
            return newPos;
        }

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="subj">The subj.</param>
        /// <param name="clip">The clip.</param>
        /// <param name="result">The result.</param>
        /// <param name="op">The op.</param>
        public static void Compute(Polygon subj, Polygon clip, Polygon result, ClippingOperations op)
        {
            var boi = new BooleanOpImp(subj, clip, result, op);
            BooleanOpImp.Run();
        }
    }
}
