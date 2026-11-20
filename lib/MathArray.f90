module MathArray
    use iso_c_binding
    implicit none
contains
    ! 1D array sum of all elements
    function r_arr_1d_sum_nobase(arr, n, status) result(res) bind(C)
        integer(c_int), value :: n
        real(c_double), intent(in) :: arr(*)
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = sum(arr(1:n))
        status = 0
    
    end function r_arr_1d_sum_nobase

    ! 1D array subtraction from the first element
    function r_arr_1d_sub_nobase(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res
        integer :: i

        res = arr(1)
        do i = 2, n
            res = res + arr(i)
        end do
        status = 0
        
    end function r_arr_1d_sub_nobase

    ! 1D array multiplication of the first element
    function r_arr_1d_mul_nobase(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = product(arr(1:n))
        status = 0
        
    end function r_arr_1d_mul_nobase

    ! 1D array division of the first element
    function r_arr_1d_div_nobase(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res
        integer :: i
    
        res = arr(1)
        do i = 2, n
            if ( arr(i) == 0 ) then
                res = 0
                status = -1
                return
            end if
            res = res / arr(i)
        end do
        status = 0
    end function r_arr_1d_div_nobase

    ! subtraction of all following elements from the first element of each column in 2D array
    subroutine r_arr_2d_sub_cols(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(b)

        integer :: i, j

        do j = 1, b
            res(j) = arr(1, j);

            do i = 2, a
                res(j) = res(j) - arr(i, j)
            end do
        end do
        status = 0

    end subroutine r_arr_2d_sub_cols

end module MathArray