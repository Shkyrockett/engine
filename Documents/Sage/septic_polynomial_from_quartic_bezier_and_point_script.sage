# Based off of the script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','cX','dX','eX','aY','bY','cY','dY','eY','t','x','y' )

B_x(t) = (1-t)^4*aX + 4*(1-t)^3*t*bX + 6*(1-t)^2*t^2*cX + 4*(1-t)*t^3*dX + t^4*eX
B_y(t) = (1-t)^4*aY + 4*(1-t)^3*t*bY + 6*(1-t)^2*t^2*cY + 4*(1-t)*t^3*dY + t^4*eY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

g = (B_x(t) - x)*B_prime_x(t) + (B_y(t) - y)*B_prime_y(t)
g = g.expand()

print "\n"
print "t^7 => ", g.coefficient(t,7), "\n"
print "t^6 => ", g.coefficient(t,6), "\n"
print "t^5 => ", g.coefficient(t,5), "\n"
print "t^4 => ", g.coefficient(t,4), "\n"
print "t^3 => ", g.coefficient(t,3), "\n"
print "t^2 => ", g.coefficient(t,2), "\n"
print "t^1 => ", g.coefficient(t,1), "\n"
print "t^0 => ", g.coefficient(t,0)
