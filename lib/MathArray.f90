module MathArray
    use iso_c_binding
    implicit none
contains
! 1D functions

    function r_arr_1d_sum_nobase(arr, n, status) result(res) bind(C)
        integer(c_int), value :: n
        real(c_double), intent(in) :: arr(*)
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = sum(arr(1:n))
        status = 0
    
    end function r_arr_1d_sum_nobase


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


    function r_arr_1d_mul_nobase(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = product(arr(1:n))
        status = 0
        
    end function r_arr_1d_mul_nobase


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


    subroutine r_arr_1d_abs(arr, n, res, status) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(n)
        
        integer :: i

        do i = 1, n
            res(i) = abs(arr(i))
        end do
        status = 0
        
    end subroutine r_arr_1d_abs


    subroutine r_arr_1d_antiabs(arr, n, res, status) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(n)
        
        integer :: i

        do i = 1, n
            res(i) = -abs(arr(i))
        end do
        status = 0
        
    end subroutine r_arr_1d_antiabs


    function r_arr_1d_min(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = minval(arr(1:n))
        status = 0
        
    end function r_arr_1d_min


    function r_arr_1d_max(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res

        res = maxval(arr(1:n))
        status = 0
    end function r_arr_1d_max


    function r_arr_1d_sqrt_sum(arr, n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_short), intent(out) :: status
        real(c_double) :: res, s

        s = sum(arr(1:n))
        if (s < 0.0_c_double) then
            res = 0.0_c_double
            status = -1
            return
        end if

        res = sqrt(s)
        status = 0
    end function r_arr_1d_sqrt_sum


    function r_arr_1d_nthroot_sum(arr, n, root_n, status) result(res) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        integer(c_int), value :: root_n
        integer(c_short), intent(out) :: status
        real(c_double) :: res, s

        s = sum(arr(1:n))
        if (s < 0.0_c_double .and. mod(root_n, 2) == 0) then
            res = 0.0_c_double
            status = -1
            return
        end if

        if (s < 0.0_c_double) then
            res = -((-s) ** (1.0_c_double / real(root_n, c_double)))
        else
            res = s ** (1.0_c_double / real(root_n, c_double))
        end if
        status = 0
    end function r_arr_1d_nthroot_sum


    subroutine r_arr_1d_unique(arr, n, res, n_unique, status) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        real(c_double), intent(out) :: res(n)
        integer(c_int), intent(out) :: n_unique
        integer(c_short), intent(out) :: status

        integer :: i, j
        logical :: is_dup

        n_unique = 0
        if (n <= 0) then
            status = -1
            return
        end if

        do i = 1, n
            is_dup = .false.
            do j = 1, n_unique
                if (arr(i) == res(j)) then
                    is_dup = .true.
                    exit
                end if
            end do
            if (.not. is_dup) then
                n_unique = n_unique + 1
                res(n_unique) = arr(i)
            end if
        end do

        status = 0
    end subroutine r_arr_1d_unique


    subroutine r_arr_1d_floor(arr, n, res, status) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        real(c_double), intent(out) :: res(n)
        integer(c_short), intent(out) :: status
        integer :: i

        do i = 1, n
            res(i) = floor(arr(i))
        end do
        status = 0
    end subroutine r_arr_1d_floor


    subroutine r_arr_1d_ceiling(arr, n, res, status) bind(C)
        real(c_double), intent(in) :: arr(*)
        integer(c_int), value :: n
        real(c_double), intent(out) :: res(n)
        integer(c_short), intent(out) :: status
        integer :: i

        do i = 1, n
            res(i) = ceiling(arr(i))
        end do
        status = 0
    end subroutine r_arr_1d_ceiling

! 2D functions

    subroutine r_arr_2d_add_cols(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(b)

        integer :: i

        do i = 1, b
            res(i) = sum(arr(1:a, i))
        end do
        status = 0

    end subroutine r_arr_2d_add_cols


    subroutine r_arr_2d_sub_cols(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(b)

        integer :: i, j

        do j = 1, b
            res(j) = arr(1, j)

            do i = 2, a
                res(j) = res(j) - arr(i, j)
            end do
        end do
        status = 0

    end subroutine r_arr_2d_sub_cols


    subroutine r_arr_2d_mul_cols(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(b)
    
        integer :: j

        do j = 1, b
            res(j) = arr(1, j)
            res(j) = res(j) * product(arr(2:a, j))
        end do
        status = 0
    end subroutine r_arr_2d_mul_cols


    subroutine r_arr_2d_div_cols(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(b)
    
        integer :: i, j

        do j = 1, b
            res(j) = arr(1, j)

            do i = 2, a
                if ( arr(i, j) == 0.0 ) then
                    status = -1
                    return
                end if
                res(j) = res(j) / arr(i, j)
            end do
        end do
        status = 0

    end subroutine r_arr_2d_div_cols


    subroutine r_arr_2d_add_rows(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(a)

        integer :: i

        do i = 1, a
            res(i) = sum(arr(i, 1:b))
        end do
        status = 0

    end subroutine r_arr_2d_add_rows


    subroutine r_arr_2d_sub_rows(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(a)

        integer :: i, j

        do i = 1, a
            res(i) = arr(i, 1)

            do j = 2, b
                res(i) = res(i) - arr(i, j)
            end do
        end do
        status = 0

    end subroutine r_arr_2d_sub_rows


    subroutine r_arr_2d_mul_rows(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(a)
    
        integer :: i

        do i = 1, a
            res(i) = product(arr(i, 1:b))
        end do
        status = 0

    end subroutine r_arr_2d_mul_rows

    subroutine r_arr_2d_div_rows(arr, a, b, res, status) bind(C)
        real(c_double), intent(in) :: arr(a, b)
        integer(c_int), value :: a, b
        integer(c_short), intent(out) :: status
        real(c_double), intent(out) :: res(a)
    
        integer :: i, j

        do i = 1, a
            res(i) = arr(i, 1)

            do j = 2, b
                if ( arr(i, j) == 0.0 ) then
                    status = -1
                    return
                end if
                res(i) = res(i) / arr(i, j)
            end do
        end do
        status = 0

    end subroutine r_arr_2d_div_rows

    

end module MathArray