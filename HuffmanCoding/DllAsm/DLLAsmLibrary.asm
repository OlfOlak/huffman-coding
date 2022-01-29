.data
    result db 8 dup (0);
.code
ConvertBytes proc
    ;movups xmm1, dword ptr [RCX]
    ;movups xmm2, dword ptr [RDX] 
    ;cmp xmm1, xmm2
    ;PCMPEQD xmm1, xmm2
    ;movups dword ptr [R8], xmm1 
    ret

ConvertBytes endp

end