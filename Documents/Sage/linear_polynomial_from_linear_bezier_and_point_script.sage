# Based on script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','aY','bY','t','x','y' )

B_x(t) = (1-t)*aX + t*bX
B_y(t) = (1-t)*aY + t*bY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

g = (B_x(t) - x)*B_prime_x(t) + (B_y(t) - y)*B_prime_y(t)
g = g.expand()

print "\n"
print "t^1 => ", g.coefficient(t,1), "\n"
print "t^0 => ", g.coefficient(t,0)
