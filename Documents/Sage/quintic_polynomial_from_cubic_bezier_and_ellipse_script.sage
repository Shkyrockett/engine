# Based on script at: http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

var( 'aX','bX','cX','dX','aY','bY','cY','dY','t','h','k', 'a', 'b', 'sin', 'cos' )

B_x(t) = (1-t)^3*aX + 3*(1-t)^2*t*bX + 3*(1-t)*t^2*cX + t^3*dX
B_y(t) = (1-t)^3*aY + 3*(1-t)^2*t*bY + 3*(1-t)*t^2*cY + t^3*dY
B_prime_x(t) = B_x.derivative(t)
B_prime_y(t) = B_y.derivative(t)

e = a^2*h^2*sin^2-2*a^2*h*k*sin*cos-2*a^2*h*B_x(t)*sin^2+2*a^2*h*B_y(t)*sin*cos+a^2*k^2*cos^2+2*a^2*k*B_x(t)*sin*cos-2*a^2*k*B_y(t)*cos^2+a^2*B_x(t)*B_x(t)*sin-2*a^2*B_x(t)*B_y(t)*sin*cos+a^2*B_y(t)*B_y(t)*cos^2+b^2*h^2*cos^2+2*b^2*h*k*sin*cos-2*b^2*h*B_x(t)*cos^2-2*b^2*h*B_y(t)*sin*cos+b^2*k^2*sin^2-2*b^2*k*B_x(t)*sin*cos-2*b^2*k*B_y(t)*sin^2+b^2*B_x(t)*B_x(t)*cos^2+2*b^2*B_x(t)*B_y(t)*sin*cos+b^2*B_y(t)*B_y(t)*sin^2-a^2*b^2
e.expand().expand()

print "\n"
print "t^5 => ", e.coefficient(t,5), "\n"
print "t^4 => ", e.coefficient(t,4), "\n"
print "t^3 => ", e.coefficient(t,3), "\n"
print "t^2 => ", e.coefficient(t,2), "\n"
print "t^1 => ", e.coefficient(t,1), "\n"
print "t^0 => ", e.coefficient(t,0)
