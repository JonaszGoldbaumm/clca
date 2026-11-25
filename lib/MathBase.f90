module MathBase
    use iso_c_binding
    implicit none
    
contains

    function r_add(a, b, status) result(res) bind(C)
        REAL(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        REAL(c_double) :: res
     
        res = a + b     
        status = 0   
    end function r_add


    function i_add(a, b, status) result(res) bind(C)
        INTEGER(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a + b
        status = 0
    end function i_add


    function r_sub(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = a - b
        status = 0
    end function r_sub


    function i_sub(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a - b
        status = 0
    end function i_sub


    function r_mul(a, b, status) result(res) bind(C)
        real(c_double), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double) :: res
    
        res = a * b
        status = 0
    end function r_mul


    function i_mul(a, b, status) result(res) bind(C)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
    
        res = a * b
        status = 0
    end function i_mul


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

    function r_sqrt(a, status) result(res) bind(C)
        real(c_double), value :: a 
        integer(c_short), intent(out) :: status
        integer(c_int) :: res
        
        if ( a < 0 ) then
            res = -1
            status = -1
            return
        end if
        res = sqrt(a)
        status = 0
    end function r_sqrt

end module MathBase