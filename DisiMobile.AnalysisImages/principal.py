import cv2
from azure.storage.blob import BlockBlobService
## Creamos una funcion "validacion" para acceder al azure blob storage
def validacion():
    
    contador=0
    dni = input("Ingresar DNI: >")
    print("\n=====> Evaluando \n")

    ## cuenta y key para el acceso a azure storage
    block_blob_service = BlockBlobService(account_name='disiblob', account_key='v8cKJJqdCxsLNmqb5oA+H0VpE5ZTejD3BKLW1GNWBXAAIv9g+kVx4Jass1e8kRnWxvEhWQ14LYVHQSA/h3bZoQ==')
    ## agregamos el blob + el directorio (dnis)
    generator = block_blob_service.list_blobs('persona',dni)
    for blob in generator:
        contador = contador + 1
        print(blob.name)
    return contador
    
datosObtenidos = validacion()

## Comprueba si existen datos en la consulta del container o azure blob storage
while(datosObtenidos == 0):
    datosObtenidos = validacion()        




#Cargamos las dos imagenes para hacer las diferencias
diff1 = cv2.imread('images/imagen1.png')
diff2 = cv2.imread('images/imagen2.png')
 
#Calculamos la diferencia absoluta de las dos imagenes
diff_total = cv2.absdiff(diff1, diff2)
 
#Buscamos los contornos
imagen_gris = cv2.cvtColor(diff_total, cv2.COLOR_BGR2GRAY)
contours,_ = cv2.findContours(imagen_gris,cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)
 
#Miramos cada uno de los contornos y, si no es ruido, dibujamos su Bounding Box sobre la imagen original
for c in contours:
    if cv2.contourArea(c) >= 20:
        posicion_x,posicion_y,ancho,alto = cv2.boundingRect(c) #Guardamos las dimensiones de la Bounding Box
        cv2.rectangle(diff1,(posicion_x,posicion_y),(posicion_x+ancho,posicion_y+alto),(0,0,255),2) #Dibujamos la bounding box sobre diff1
 
while(1):
    #Mostramos las imagenes. ESC para salir.
    cv2.imshow('Imagen1', diff1)
    cv2.imshow('Imagen2', diff2)
    cv2.imshow('Diferencias detectadas', diff_total)
    tecla = cv2.waitKey(5) & 0xFF
    if tecla == 27:
        break
 
cv2.destroyAllWindows()
