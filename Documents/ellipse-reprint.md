
# A closed form solution for the intersections of two ellipses

Posted by [Elliot Noma](https://elliotnoma.wordpress.com/author/elliotnoma/) on April 10, 2013 at: <https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/> 

<svg width="260" height="260">
 <g stroke-width="1" stroke="black" fill="transparent"  transform="translate(0 30) rotate(-45 105 105)">
  <ellipse cx="105" cy="105" rx="100" ry="50" id="Ellipse"/>
  <circle cx="150" cy="60" r="3" fill="red" id="Point"/>
  <circle cx="60" cy="60" r="3" fill="red" id="Point"/>
 </g>
 <g stroke-width="1" stroke="black" fill="transparent"  transform="translate(0 30) rotate(45 105 105)">
  <ellipse cx="105" cy="105" rx="100" ry="50" id="Ellipse"/>
  <circle cx="150" cy="60" r="3" fill="red" id="Point"/>
  <circle cx="150" cy="150" r="3" fill="red" id="Point"/>
 </g>
</svg>

There intersections of two ellipses are determined using the following steps.

1. Rewrite each ellipse as a second degree equations in x and y coordinates.

2. Convert the intersection of the two second degree equations into a fourth degree polynomial which can be solved using closed form methods.

3. Compute the x values for each y.  

In theory all these steps can be computed using closed form solutions.

The ellipse centered at $(a, c)$ with axes determined by coefficients b and d is specified:

$\frac{\left(x’–a\right)^2}{b}+\frac{\left(y’ – c\right)^2}{d}=1$

In addition, the xy coordinates can be rotated by:

$x’=x\cos(q)–y\sin(q)$  

$y’=x\sin(q)+y\cos(q)$  

To simplify the notation, set $A = \cos(q)$ and $B = \sin(q)$. Substituting for $x’$ and $y’$ produces the general formula for an ellipse:

$\frac{\left(Ax –By – a\right)^2}{b} + \frac{\left(Bx + Ay – c\right)^2}{d} = 1$

This can be rewritten:

$\left(\frac{A^2}{b}+\frac{B^2}{d}\right)x^2+\left(\frac{-2AB}{b}+\frac{2AB}{d}\right)xy+\left(\frac{B^2}{b}+\frac{A^2}{d}\right)y^2+\left(\frac{-2aA}{b}-\frac{2cB}{d}\right)x+\left(\frac{2aB}{b}-\frac{2cA}{d}\right)y + \frac{a^2}{b}+\frac{c^2}{d}-1 = 0$

This method first translates and then rotates the ellipse.

An alternative method first rotates the ellipse then re-centers it. With $A = \cos(q)$ and $B = \sin(q)$, the polynomial coefficients for the initial rotation are:

$A_A = \frac{A^2}{b} + \frac{B^2}{d}$

$B_B = \frac{-2AB}{b} + \frac{2AB}{d}$

$C_C = \frac{B^2}{b} + \frac{A^2}{d}$

$F_F = -1$

In the equation $A_A”x^2 + B_Bx”y” + C_Cy”^2 + F = 0$.
By substituting $x” = x – a$ and $y” = y – c$ to shift the center of the ellipse and using the rotates variables $A_A$, $B_B$, $C_C$, we get the polynomial describing the ellipse:

$A_Ax^2 + BBxy + C_Cy^2 -(2A_A a + B_B c)x – (B_B a + 2C_C c)y + A_A a^2 + B_B ac + C_C c^2 – 1 = 0$

We have now standardized the equations defining the individual ellipses.

1. The two ellipses are now written as quadratic conic sections. Their xy coordinates can be solved using a solution from <https://web.archive.org/web/20130608140915/http://www.math.niu.edu/~rusin/known-math/99/2ellipses>. This formula solves for y values using the coefficients for each ellipse determined in the previous step. The coefficients for each ellipse, $f_1$ and $f_2$, are identified in the following equalities

$f_1 :  ax^2 + bxy + cy^2 + dx + ey + f = 0$

$f_2 :  a_1x^2 + b_1xy + c1y^2 + d_1x + e_1y + fq = 0$

The solution is the zeros of a fourth degree polynomial $z0 + z_1y + z2y^2 + z_3y^3 + z_4y^4  = 0$
with the following coefficients derived from the expressions of $f_1$ and $f_2$,

$z_0 = fad_1^2+a^2fq^2-dad_1f_q+a_1^2f^2-2af_qa_1f-dd_1a_1f+a_1d^2f_q$

$z_1 = e_1d^2a_1-f_qd_1ab-2af_qa_1e-fa_1b_1d+2d_1b_1af+2e_1f-qa^2+d_1^2ae-e_1d_1ad-2ae_1a_1f-fa_1d_1b+2fea_1^2-f_qb_1ad-ea_1d_1d+2f_qba_1d$

$z_2 = e_1^2a^2+2c_1f_qa^2-ea_1d_1b+f_qa_1b^2-ea_1b_1d-f_qb_1ab-2ae_1a_1e+2d_1b_1ae-c_1d_1ad-2ac_1a_1f+b_1^2af+2e_1ba_1d+e^2a_1^2-ca_1d_1d-e_1b_1ad+2fca_1^2-fa_1b_1b+c_1d^2a_1+d_1^2ac-e_1d_1ab-2af_qa_1c$

$z_3 = -2aa_1ce_1+e_1a_1b^2+2c_1ba_1d-ca_1b_1d+b_1^2ae-e_1b_1ab-2ac_1a_1e-ea_1b_1b-c_1b_1ad+2e_1c1a^2+2eca_1^2-ca_1d_1b+2d_1b_1ac-c_1d_1ab$

$z_4 = a^2c_1^2-2ac_1a_1c+a_1^2c^2-bab_1c_1-bb_1a_1c+b^2a_1c_1+cab_1^2$

There are analytic solutions to this equation which produce four solutions in the complex plane. Only the solutions with zero imaginary components are of interest when computing the intersection points.

The corresponding x values are computed by:

$x = \frac{-(af_q+ac_1y^2-a_1cy^2+ae_1y-a_1ey-a_1f)}{ab_1y+ad_1-a_1by-a_1d}$

There are, however, cases where the denominator is zero when the main axes of the ellipses are horizontal and vertical.  In this case we can solve for x using the formula:

$b_b = by + d$

$c_c = cy^2 + ey + f$

$x = \frac{-b_b + sqrt(b_b^2 – 4ac_c)}{2a} or x = \frac{-b_b – sqrt(b_b^2 – 4ac_c)}{2a}$

There are two intersection points with identical y values and positive and negative solutions for x.

By solving for the intersections of two ellipses, we have also solved for the intersections of any two conic sections showing there are a maximum of four intersections for any two distinct conic figures.
