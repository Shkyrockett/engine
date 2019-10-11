# Based on script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','cX','dX','aY','bY','cY','dY','t','h','k', 'a', 'b', 'sin', 'cos' )

B_x(t) = (1-t)^3*aX + 3*(1-t)^2*t*bX + 3*(1-t)*t^2*cX + t^3*dX
B_y(t) = (1-t)^3*aY + 3*(1-t)^2*t*bY + 3*(1-t)*t^2*cY + t^3*dY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

e = ((B_x(t)-h)*cos+(B_y(t)-k)*sin)^2*B_prime_x(t)/a^2+((B_x(t)-h)*sin-(B_y(t)-k)*cos)^2/b^2*B_prime_y(t)-1
e.expand()

print "\n"
print "t^5 => ", e.coefficient(t,5), "\n"
print "t^4 => ", e.coefficient(t,4), "\n"
print "t^3 => ", e.coefficient(t,3), "\n"
print "t^2 => ", e.coefficient(t,2), "\n"
print "t^1 => ", e.coefficient(t,1), "\n"
print "t^0 => ", e.coefficient(t,0)

g = (B_x(t) - h)^2*B_prime_x(t) + (B_y(t) - k)^2*B_prime_y(t)
g = g.expand()

print "\n"
print "t^5 => ", g.coefficient(t,5), "\n"
print "t^4 => ", g.coefficient(t,4), "\n"
print "t^3 => ", g.coefficient(t,3), "\n"
print "t^2 => ", g.coefficient(t,2), "\n"
print "t^1 => ", g.coefficient(t,1), "\n"
print "t^0 => ", g.coefficient(t,0)
