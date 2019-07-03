# Based on script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','cX','aY','bY','cY','t','x','y' )

B_x(t) = (1-t)^2*aX + 2*(1-t)*t*bX + t^2*cX
B_y(t) = (1-t)^2*aY + 2*(1-t)*t*bY + t^2*cY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

g = (B_x(t) - x)*B_prime_x(t) + (B_y(t) - y)*B_prime_y(t)
g = g.expand()

print "\n"
print "t^3 => ", g.coefficient(t,3), "\n"
print "t^2 => ", g.coefficient(t,2), "\n"
print "t^1 => ", g.coefficient(t,1), "\n"
print "t^0 => ", g.coefficient(t,0)
