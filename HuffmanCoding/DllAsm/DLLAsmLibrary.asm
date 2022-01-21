.code
ConvertBytes proc
    mov al, [RCX]
    cbw
    mov [RDX], ax
    ret

ConvertBytes endp

end