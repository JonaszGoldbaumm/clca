module MathBase
    use iso_c_binding
    implicit none
    
contains
    ! addition of two values
    function r_add(a, b, status) result(res) bind(C)
        REAL(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        REAL(c_double) :: res
     
        res = a + b     
        status = 0   
    end function r_add

    ! addition of two values
    function i_add(a, b, status) result(res) bind(C)
        INTEGER(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a + b
        status = 0
    end function i_add

    ! subtraction of b value from a
    function r_sub(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = a - b
        status = 0
    end function r_sub

    ! subtraction of b value from a
    function i_sub(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a - b
        status = 0
    end function i_sub

    ! multiplication of two values
    function r_mul(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = a * b
        status = 0
    end function r_mul

    ! multiplication of two values
    function i_mul(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a * b
        status = 0
    end function i_mul

    ! division of a value by b value
    function r_div(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
        if ( b == 0 ) then
            res = 0
            status = -1
            return
        end if
        res = a / b
        status = 0
    end function r_div

    ! division of a value by b value
    function i_div(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
        if ( b == 0.0 ) then
            res = 0
            status = -1
            return
        end if

        res = a / b
        status = 0
    end function i_div

    function r_mod(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = mod(a, b)
        status = 0
    end function 

    function i_mod(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = mod(a, b)
        status = 0
    end function i_mod

    function r_modulo(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = modulo(a, b)
        status = 0
    end function r_modulo

    function i_modulo(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = modulo(a, b)
        status = 0
    end function i_modulo

end module MathBase