# Based off of the script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','cX','dX','eX','fX','aY','bY','cY','dY','eY','fY','t','x','y' )

B_x(t) = (1-t)^5*aX + 5*t*(1-t)^4*bX + 10*t^2*(1-t)^3*cX + 10*t^3*(1-t)^2*dX + 5*t^4*(1-t)*eX + t^5*fX
B_y(t) = (1-t)^5*aY + 5*t*(1-t)^4*bY + 10*t^2*(1-t)^3*cY + 10*t^3*(1-t)^2*dY + 5*t^4*(1-t)*eY + t^5*fY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

g = (B_x(t) - x)*B_prime_x(t) + (B_y(t) - y)*B_prime_y(t)
g = g.expand()

print "\n"
print "t^9 => ", g.coefficient(t,9), "\n"
print "t^8 => ", g.coefficient(t,8), "\n"
print "t^7 => ", g.coefficient(t,7), "\n"
print "t^6 => ", g.coefficient(t,6), "\n"
print "t^5 => ", g.coefficient(t,5), "\n"
print "t^4 => ", g.coefficient(t,4), "\n"
print "t^3 => ", g.coefficient(t,3), "\n"
print "t^2 => ", g.coefficient(t,2), "\n"
print "t^1 => ", g.coefficient(t,1), "\n"
print "t^0 => ", g.coefficient(t,0)
