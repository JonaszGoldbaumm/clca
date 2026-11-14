module MathBase
    use iso_c_binding
    implicit none
    
contains
    
    function r_add(a, b) result(res) bind(C)
        REAL(c_double), value :: a, b
        REAL(c_double) :: res
     
        res = a + b        
    end function r_add

    function i_add(a, b) result(res) bind(C)
        INTEGER(c_int), value :: a, b
        integer(c_int) :: res
    
        res = a + b
    end function i_add

    function r_sub(a, b) result(res) bind(C)
        real(c_double), value :: a, b
        real(c_double) :: res
    
        res = a - b
    end function r_sub

    function i_sub(a, b) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_int) :: res
    
        res = a - b
    end function i_sub

    function r_mul(a, b) result(res) bind(C)
        real(c_double), value :: a, b
        real(c_double) :: res
    
        res = a * b
    end function r_mul

    function i_mul(a, b) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_int) :: res
    
        res = a * b
    end function i_mul

    function r_div(a, b) result(res) bind(C)
        real(c_double), value :: a, b
        real(c_double) :: res
        if ( b == 0 ) then
            res = 0
            return
        end if
        res = a / b
    end function r_div

    function i_div(a, b) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_int) :: res
        if ( b == 0.0 ) then
            res = 0
            return
        end if

        res = a / b
    end function i_div

    function r_mod(a, b) result(res) bind(C)
        real(c_double), value :: a, b
        real(c_double) :: res
    
        res = mod(a, b)
    end function 

    function i_mod(a, b) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_int) :: res
    
        res = mod(a, b)
    end function i_mod

    function r_modulo(a, b) result(res) bind(C)
        real(c_double), value :: a, b
        real(c_double) :: res
    
        res = modulo(a, b)
    end function r_modulo

    function i_modulo(a, b) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_int) :: res
    
        res = modulo(a, b)
    end function i_modulo

end module MathBase