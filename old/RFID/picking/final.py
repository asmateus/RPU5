from tkinter import *
import os
import time
import timeit
#import RPi.GPIO as GPIO
from subprocess import call
orden = 0

def pycking():

    #txt02 = "E:\Picking Jessel Camargo\Pick2Ligth\Pick2light Código Completo y Arvhivos Necesarios/epc.txt"
    #txt01 = "E:\Picking Jessel Camargo\Pick2Ligth\Pick2light Código Completo y Arvhivos Necesarios/pedido1.txt"
    txt02 = "E:\Picking Jessel Camargo\Pick2Ligth\Pick2light Código Completo y Arvhivos Necesarios/epc.txt"
    txt01 = "E:\Picking Jessel Camargo\Pick2Ligth\Pick2light Código Completo y Arvhivos Necesarios/pedido1.txt"
    global clase
    global inicio
    global v
    global orden

    orden = orden + 1
    if orden > 1:
        v.destroy() #Destruir
        root.destroy()
    v=Tk()
    v.focus_set()
    v.grab_set()
    v.config(bg="#274090")
    v.geometry("320x240+0+0")
    v.title("Autorización")

    ##led  con matriz 32X8
    #
    #def led (fila, colum):
    ###-------------------------------------
    #    pincolum = [4, 17, 27, 22, 23]
    #    pinfila = [14, 15, 18]
    #
    #    GPIO.cleanup()
    #    GPIO.setmode(GPIO.BCM)
    #    for s1 in range(5):
    #        GPIO.setup(pincolum[s1], GPIO.OUT) # columna
    #    for m in range(3):
    #        GPIO.setup(pinfila[m], GPIO.OUT) # fila
    #
    #    fila = '00'+bin(fila)
    #    colum = '00'+bin(colum)
    #    for i in range(3):
    #        if fila[-i-1] == '1':
    #            GPIO.output(pinfila[i], True)
    #            #print(pinfila[i], 1)
    #        else:
    #            GPIO.output(pinfila[i], False)
    #            #print(pinfila[i], 0)
    #    for f in range(5):
    #        if colum[-f-1] == '1':
    #            GPIO.output(pincolum[f], True)
    #            #print(pincolum[f], 1)
    #        else:
    #            GPIO.output(pincolum[f], False)
    #            #print(pincolum[f], 0)
    #    #print ("Encendido de Led", fila, colum ) # prueba de funcionamiento
    #


    #led 16X16
    #    def led (fila, colum):
    #        pincolum = [4, 17, 27, 22]
    #        pinfila = [14, 15, 23, 18]
    #        GPIO.cleanup()
    #
    #        GPIO.setmode(GPIO.BCM)
    #        for s in range(4):
    #            GPIO.setup(pincolum[s], GPIO.OUT) # columna
    #            GPIO.setup(pinfila[s], GPIO.OUT) # fila
    #
    #        fila = '00' + bin(fila)
    #        colum = '00' + bin(colum)
    #        for i in range(4):
    #            if fila[-i-1] == '1':
    #                GPIO.output(pinfila[i], True)
    #            else:
    #                GPIO.output(pinfila[i], False)
    #            if colum[-i-1] == '1':
    #                GPIO.output(pincolum[i], True)
    #            else:
    #                GPIO.output(pincolum[i], False)
    ##print ("Ejecucion finalizada")
    #    GPIO.cleanup()

    ###    ESTO ESTÁ COMENTADO PORQUE ES DE LA RASPBERRY, PARA ENCENDER LOS LED'S
    ###    ES LO QUE ENVÍA LA ORDEN A CADA PIN
    ###    SE PUEDE ESCOGER ENTRE DOS TIPO DE CONFIFURACION MATRICIALES DE LED
    ###    DESCOMENTE LA QUE SE VA A USA 16x16 o 32X8
    ID = StringVar()
    ID.set("") ## definir variable
    caja = Entry(v, textvariable=ID, width=20, font=(16))
    caja.focus() ## enfoca la entrada de texto
    caja.bind('<Return>',(lambda event: (iden()))) #Llama al botÃ³n de identificar con un Enter
    caja.place(x=100, y=5)
##    la funcion texto toma las lineas de un archivo texto modificado por base
##    de datos para ingresarlas en un vector
##    adicionalmente entrega el numero de lineas y borra los espaciados innecesarios
    def texto(txt1, clase):
        ## Archivo 1--pedidos--(###;#;#) cada linea
        if clase == 0:
            archivo = open(txt1, "r")
            l = list(archivo)
            archivo.close()  # cerar el archivo para evitar consumo de recursos
            e = 0
            for i in range(len(l)):
                b = l[i]
                if b != '\n':
                    e = e + 1
                i = i + 1
            arc = list(range(e))
            for j in range(i):
                b = l[j]
                if b != '\n':
                    arc[j] = b[0:7]
                j = j + 1
            return arc
        ##--- Archivo 2---epc 12 caracteres acordado con base de datos
        elif clase == 1:
            archivo = open(txt1, "r")
            l = list(archivo)
            archivo.close()
            i = 0
            e = 0
            for i in range(len(l)):
                b = l[i]
                if b != '\n':
                    e = e + 1
                i = i + 1
            arc2 = list(range(e))
            for j in range(i):
                b = l[j]
                if b != '\n':
                    arc2[j] = b[0:12]# aqui se puede cambiar el # de caracteres
                j = j + 1
            return arc2
        else:## evita errores a la ora de colocar el tipo de caso
            print("ERROR")
        #os.remove("nose.txt") ##eliminar text, consume recursos
    #f = texto(txt02, 1)##prueba
    #print(f)###prueba
    global counter
    counter=0
    ##
    def funcion():
        global root
        root = Tk()
        root.focus_set()
        root.geometry("320x240+0+0")
        root.config(bg="#274090")
        root.title("Pedido")
        v.iconify() #Minimizar ventana
        h1 = texto(txt01, 0)
        d = len(h1)
        global k1
        k1 = 0

        def funcion2():
            global k1
            global orden
            if k1 < d:
                lineas = h1[k1]
                lblE = Label(root, text="Estación: ", background="#1895DA",
                    relief=FLAT, font=(16), fg="#FFFFFF")
                lblE2 = Label(root, text=lineas[6], background="#1895DA",
                    relief=FLAT, font=(16), fg="#FFFFFF")
                lblP = Label(root, text="Pedido: ", background="#1895DA",
                    relief=FLAT, font=(16), fg="#FFFFFF")
                lblP2 = Label(root, text=lineas[4] + " Ficha # " + lineas[0:3],
                    background="#1895DA", relief=FLAT, font=(16), fg="#FFFFFF")
                lblE.place(x=5, y=5)
                lblE2.place(x=90, y=5)
                lblP.place(x=5, y=35)
                lblP2.place(x=76, y=35)
                npieza = int(lineas[0:3])## numero de la pieza

                ###----ubicacion de piezas--
                ##--esto se hace para encontrar la posicion de la pieza
                ##--dependiendo de la matiz usada.
                ## el # de pieza "n" de 0 hasta n depende  de la matriz.
                ## para este caso n va de 1 hasta n
                h = 1
                f1 = 0
                for fil in range(6):
                    for col in range(18):
                        if (h >= npieza) | (npieza == 1):
                            f1 = 1
                            break
                        h = h + 1
                    if (f1 == 1):
                        break
                print((fil, col))
                #led(fil, col)
                bnext.config(text="Siguiente")
            else:
                horafinal = time.strftime("%X")
                finish = timeit.default_timer()
                demora = finish - start
                #print (horainicio)
                #print (horafinal)
                #print (start, finish, demora)
                tiempo = open('Tiempo.txt','w')
                tiempo.write(horainicio)
                tiempo.write("\n")
                tiempo.write(horafinal)
                tiempo.write("\n")
                tiempo.write(str(demora))
                tiempo.close()
                bnext.destroy() #modificaciÃ³n para quitar el boton
                bfinalizar = Button(root, text="Finalizar", command=pycking,
                    relief=FLAT, font=(16), bg = "#1895DA", width=25, height=3, fg="#FFFFFF")
                bfinalizar.place(x=20, y=90)
            k1 = k1 + 1
        bnext = Button(root, text="Iniciar Pedidos", relief=FLAT,
            command=funcion2, font=(16),
            bg="#1895DA", width=25, height=3, fg="#FFFFFF")
        bnext.place(x=20, y=90)
        lblorden = Label(root, text="Orden: # " + str(orden), background="#1895DA",
                    relief=FLAT, font=(16),fg="#FFFFFF").place(x=212, y=35)
        lbltime = Label(root,text="Tiempo:", background="#1895DA",
                        relief=FLAT, font=(16),fg="#FFFFFF").place(x=160, y=5)
        #Representacion de la hora
        horainicio=time.strftime("%X")
        start = timeit.default_timer()
        lbltime1 = Label(root, text=str(horainicio), background="#1895DA",
                         relief=FLAT, font=(16),fg="#FFFFFF").place(x=230, y=5)

        #root.wait_window(root)
        root.mainloop()
    ## la funcion iden es la primera ventana que aparece para hacer
    ## identificacion del usuario
    def iden():
        ##se revisa los datos que estan en epc.txt y se conpara con lo que
        ##ingresa el usuario
        f = texto(txt02, 1)
        k = 0
        p = len(f)
        auto = 0
        for k in range(p):
            t = f[k]
            if t != ID.get():
                auto = 0
            else:
                auto = 1
                break
        if auto != 1:
            lblError = Label(v, text="No Autorizado", background="#FFFF00",
                relief=FLAT, font=(16), fg="red")
            lblError.place(x=100, y=50)
            ID.set("")
            bstart.config(text="Volver a Intentarlo")
        ## si es autorizado, abre ventana de pedido que es la funcion funcion
        else:
            lblAuto = Label(v, text="  Autorizado   ", background="#1895DA",
                relief=FLAT, font=(16), fg="#FFFF00")
            lblAuto.place(x=100, y=50)
            bstart.destroy() #modificaciÃ³n para quitar el boton
            funcion()
    bstart = Button(v, text="Identificar", command=iden, relief=FLAT,
        width=25, height=3, bg="#1895DA", font=(16),fg="#FFFFFF")
    bstart.place(x=20, y=90)
    lblU = Label(v,text="ID Usuario:", background="#1895DA",
                 relief=FLAT, font=(16), fg="#FFFFFF").place(x=2, y=5)
    v.mainloop()
pycking()
