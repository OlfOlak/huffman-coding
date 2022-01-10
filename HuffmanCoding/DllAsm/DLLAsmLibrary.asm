.data
ptr1 dq 0
ptr2 dq 0
ptr3 dq 0
two real4 2.0

.code
zad1 proc
	xor rax, rax  
	mov r10, rcx
	mov rcx, rdx
	
	_loop:
	add eax, dword ptr [r10 + rcx * sizeof dword] 
	dec rcx
	cmp rcx, -1
	jne _loop

	ret
zad1 endp

zad2 proc
	mov r10, [rcx]
	mov ptr1, r10
	mov r10, [rcx + 8]
	mov ptr2, r10
	mov r10, [rcx + 16]
	mov ptr3, r10
	mov rcx, 0

	_loop1:
	mov r10, ptr1
	movd xmm0, real4 ptr [r10 + rcx * sizeof real4]
	mov r10, ptr2
	movd xmm1, real4 ptr [r10 + rcx * sizeof real4]
	addss xmm0, xmm1
	movd xmm1, two
	divss xmm0, xmm1
	mov r10, ptr3
	movd real4 ptr [r10 + rcx * sizeof real4], xmm0
	inc rcx
	cmp rcx, 16
	jne _loop1

	ret
zad2 endp

END
