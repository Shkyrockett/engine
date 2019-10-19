#Test

$\frac{(x-h)^2}{a^2}+\frac{(y-k)^2}{b^2}=1$

$\frac{((x-h)\cos{(\alpha)}+(y-k)\sin{(\alpha)})^2}{a^2}+\frac{((x-h)\sin{(\alpha)}-(y-k)\cos{(\alpha)})^2}{b^2}=1$

$x=A_x(1-t)^{3}+B_x3t(1-t)^{2}+C_x3t^{2}(1-t)+D_xt^{3}$

$y=A_y(1-t)^{3}+B_y3t(1-t)^{2}+C_y3t^{2}(1-t)+D_yt^{3}$

$\frac{((A_x(1-t)^{3}+B_x3t(1-t)^{2}+C_x3t^{2}(1-t)+D_xt^{3}-h)\cos{(\alpha)}+(A_y(1-t)^{3}+B_y3t(1-t)^{2}+C_y3t^{2}(1-t)+D_yt^{3}-k)\sin{(\alpha)})^2}{a^2}+\frac{((A_x(1-t)^{3}+B_x3t(1-t)^{2}+C_x3t^{2}(1-t)+D_xt^{3}-h)\sin{(\alpha)}-(A_y(1-t)^{3}+B_y3t(1-t)^{2}+C_y3t^{2}(1-t)+D_yt^{3}-k)\cos{(\alpha)})^2}{b^2}=1$

$\frac{((A_x(1-t)^{3}+B_x3t(1-t)^{2}+C_x3t^{2}(1-t)+D_xt^{3}-h)\cos{(\alpha)}+(A_y(1-t)^{3}+B_y3t(1-t)^{2}+C_y3t^{2}(1-t)+D_yt^{3}-k)\sin{(\alpha)})^2}{a^2}+\frac{((A_x(1-t)^{3}+B_x3t(1-t)^{2}+C_x3t^{2}(1-t)+D_xt^{3}-h)\sin{(\alpha)}-(A_y(1-t)^{3}+B_y3t(1-t)^{2}+C_y3t^{2}(1-t)+D_yt^{3}-k)\cos{(\alpha)})^2}{b^2}=1$

$h^2\cos^2{(\alpha)}+2hk\sin{(\alpha)}\cos{(\alpha)}-2hx\cos^2{(\alpha)}-2hy\sin{(\alpha)}\cos{(\alpha)}+k^2\sin{(\alpha)}-2kx\sin{(\alpha)}\cos{(\alpha)}-2ky\sin^2{(\alpha)}+x^2\cos^2{(\alpha)}+2xy\sin{(\alpha)}\cos{(\alpha)}+y^2\sin^2{(\alpha)}$

$h^2\sin^2{(\alpha)}-2hk\sin{(\alpha)}\cos{(\alpha)}-2hx\sin^2{(\alpha)}+2hy\sin{(\alpha)}\cos{(\alpha)}+k^2\cos^2{(\alpha)}+2kx\sin{(\alpha)}\cos{(\alpha)}-2ky\cos^2{(\alpha)}+x^2\sin^2{(\alpha)}-2xy\sin{(\alpha)}\cos{(\alpha)}+y^2\cos^2{(\alpha)}$

$\frac{h^2\cos^2{(\alpha)}+2hk\sin{(\alpha)}\cos{(\alpha)}-2hx\cos^2{(\alpha)}-2hy\sin{(\alpha)}\cos{(\alpha)}+k^2\sin{(\alpha)}-2kx\sin{(\alpha)}\cos{(\alpha)}-2ky\sin^2{(\alpha)}+x^2\cos^2{(\alpha)}+2xy\sin{(\alpha)}\cos{(\alpha)}+y^2\sin^2{(\alpha)}}{a^2}+\frac{h^2\sin^2{(\alpha)}-2hk\sin{(\alpha)}\cos{(\alpha)}-2hx\sin^2{(\alpha)}+2hy\sin{(\alpha)}\cos{(\alpha)}+k^2\cos^2{(\alpha)}+2kx\sin{(\alpha)}\cos{(\alpha)}-2ky\cos^2{(\alpha)}+x^2\sin^2{(\alpha)}-2xy\sin{(\alpha)}\cos{(\alpha)}+y^2\cos^2{(\alpha)}}{b^2}-1=0$

$(\frac{x^2\cos^2{(\alpha)}}{a^2}+\frac{x^2\sin^2{(\alpha)}}{b^2})+(\frac{2xy\sin{(\alpha)}\cos{(\alpha)}}{a^2}-\frac{2xy\sin{(\alpha)}\cos{(\alpha)}}{b^2})+(\frac{y^2\sin^2{(\alpha)}}{a^2}+\frac{y^2\cos^2{(\alpha)}}{b^2})-(\frac{2hx\cos^2{(\alpha)}}{a^2}-\frac{2hx\sin^2{(\alpha)}}{b^2})-(\frac{2hy\sin{(\alpha)}\cos{(\alpha)}}{a^2}+\frac{2hy\sin{(\alpha)}\cos{(\alpha)}}{b^2})+(\frac{h^2\cos^2{(\alpha)}}{a^2}+\frac{h^2\sin^2{(\alpha)}}{b^2})+(\frac{k^2\sin{(\alpha)}}{a^2}+\frac{k^2\cos^2{(\alpha)}}{b^2})+(\frac{2hk\sin{(\alpha)}\cos{(\alpha)}}{a^2}-\frac{2hk\sin{(\alpha)}\cos{(\alpha)}}{b^2})-(\frac{2kx\sin{(\alpha)}\cos{(\alpha)}}{a^2}+\frac{2kx\sin{(\alpha)}\cos{(\alpha)}}{b^2})-(\frac{2ky\sin^2{(\alpha)}}{a^2}-\frac{2ky\cos^2{(\alpha)}}{b^2})-1=0$

$$
\begin{bmatrix}
x_a^2b^2&y_a^2a^2&0&0&0&0&0\\
2x_ax_bb^2&2y_ay_ba^2&0&0&0&0&0\\
2x_ax_cb^2&2y_ay_ca^2&x_b^2b^2&y_b^2a^2&0&0&0\\
2x_ax_db^2&2y_ay_da^2&-2x_ab^2h&-2y_aa^2k&2x_bx_cb^2&2y_by_ca^2&0\\
2x_bx_db^2&2y_by_da^2&-2x_bb^2h&-2y_ba^2k&x_c^2b^2&y_c^2a^2&0\\
2x_cx_db^2&2y_cy_da^2&-2x_cb^2h&-2y_ca^2k&0&0&0\\
x_d^2b^2&y_d^2a^2&-2x_db^2h&-2y_da^2k&h^2b^2&k^2a^2&-a^2b^2
\end{bmatrix}
$$

$\begin{bmatrix}x_a&y_a\\x_b&y_b\\x_c&y_c\\x_d&y_d\end{bmatrix}\begin{bmatrix}b^2&a^2\\h&k\\cos{\theta}&sin{\theta}\end{bmatrix}$

```cs
(
a: ((ax  ax)  (b  b)) + ((ay  ay)  (a  a)),
b: 2d  ((ax  bx  (b  b)) + (ay  by  (a  a))),
c: 2d  (ax  cx  (b  b)) + 2d  (ay  cy  (a  a)) + ((bx  bx)  (b  b)) + ((by  by)  (a  a)),
d: (2d  ax  (b  b)  (dx - h)) + (2d  ay  (a  a)  (dy - k)) + 2d  (bx  cx  (b  b)) + 2d  (by  cy  (a  a)),
e: (2d  bx  (b  b)  (dx - h)) + (2d  by  (a  a)  (dy - k)) + ((cx  cx)  (b  b)) + ((cy  cy)  (a  a)),
f: (2d  cx  (b  b)  (dx - h)) + (2d  cy  (a  a)  (dy - k)),
g: (dx  dx  (b  b)) - (2d  dy  k  (a  a)) - (2d  dx  h  (b  b)) + ((dy  dy)  (a  a)) + ((h  h)  (b  b)) + ((k  k)  (a  a)) - ((a  a)  (b  b))
)
```

<https://math.stackexchange.com/a/436276>  
The parametric equation for B is $B(t)=(1−t)^3P_0+3t(1−t)^2P_1+3t^2(1−t)P_2+t^3P_3$. The equation for a circle is $(x-h)^2+(y-k)^2=r^2$. By projecting that onto x and y and adding the constraint of the circle's parametric equation you get a sextic  

$$((1−t)^3x_0+…+t^3x_3−h)^2+((1−t)3y_0+…+t^3y_3−k)^2=r^2$$  

In general this won't be soluble algebraically, so you'll have to solve it numerically using a method like Newton-Raphson.

The convex hull property of Bézier curves and de Casteljau's algorithm give a good heuristic. Except when the curves almost touch or overlap only slightly, you'll get a quick answer; but you'll need to limit the level of recursion to handle the borderline cases (and that's where the heuristic element comes in).

The following is the equation for an Ellipse:

$$\frac{((x-h)\cos{(\alpha)}+(y-k)\sin{(\alpha)})^2}{a^2}+\frac{((x-h)\sin{(\alpha)}-(y-k)\cos{(\alpha)})^2}{b^2}=1$$  

Let's remove the division:

$$b^2((x-h)\cos(\alpha)+(y-k)\sin(\alpha))^2+a^2((x-h)\sin(\alpha)-(y-k)\cos(\alpha))^2=a^2b^2$$

Then let's expand:

$$a^2h^2\sin^2(\alpha)-2a^2hk\sin(\alpha)\cos(\alpha)-2a^2hx\sin^2(\alpha)+2a^2hy\sin(\alpha)\cos(\alpha)+a^2k^2\cos^2(\alpha)+2a^2kx\sin(\alpha)\cos(\alpha)-2a^2ky\cos^2(\alpha)+a^2x^2\sin(\alpha)-2a^2xy\sin(\alpha)\cos(\alpha)+a^2y^2\cos^2(\alpha)+b^2h^2\cos^2(\alpha)+2b^2hk\sin(\alpha)\cos(\alpha)-2b^2hx\cos^2(\alpha)-2b^2hy\sin(\alpha)\cos(\alpha)+b^2k^2\sin^2(\alpha)-2b^2kx\sin(\alpha)\cos(\alpha)-2b^2ky\sin^2(\alpha)+b^2x^2\cos^2(\alpha)+2b^2xy\sin(\alpha)\cos(\alpha)+b^2y^2\sin^2(\alpha)=a^2b^2$$

Let's substitute x and y for the x and y parametric equations for the Bezier curve.

$$\frac{(((1−t)^3x_0+3t(1−t)^2x_1+3t^2(1−t)x_2+t^3x_3-h)\cos{(\alpha)}+((1−t)^3y_0+3t(1−t)^2y_1+3t^2(1−t)y_2+t^3y_3-k)\sin{(\alpha)})^2}{a^2}+\frac{(((1−t)^3x_0+3t(1−t)^2x_1+3t^2(1−t)x_2+t^3x_3-h)\sin{(\alpha)}-((1−t)^3y_0+3t(1−t)^2y_1+3t^2(1−t)y_2+t^3y_3-k)\cos{(\alpha)})^2}{b^2}=1$$  

Let's remove the 

$$b^2(((1−t)^3x_0+3t(1−t)^2x_1+3t^2(1−t)x_2+t^3x_3-h)\cos{(\alpha)}+((1−t)^3y_0+3t(1−t)^2y_1+3t^2(1−t)y_2+t^3y_3-k)\sin{(\alpha)})^2+a^2(((1−t)^3x_0+3t(1−t)^2x_1+3t^2(1−t)x_2+t^3x_3-h)\sin{(\alpha)}-((1−t)^3y_0+3t(1−t)^2y_1+3t^2(1−t)y_2+t^3y_3-k)\cos{(\alpha)})^2=a^2b^2$$  



