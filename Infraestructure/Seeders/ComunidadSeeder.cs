using backend_agua.Infraestructure.Database;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace backend_agua.Infraestructure.Seeders;

public static class ComunidadSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Si ya existen comunidades, no re-ejecutamos para garantizar integridad
        if (context.Comunidades.Any()) return;

        // --- PARROQUIA: ALTAGRACIA ---

        SeedComunidadesParaComuna(context, "AGROECOLOGICA Y MINERA GRAN MARISCAL DE AYACUCHO", new[] {
            "FRANCISCO DE MIRANDA Y VILLA LOS PRIMOS",
            "LA LLANADA VIEJA SECTOR GRAN MARISCAL",
            "LA LLANADA VIEJA SECTOR SAN JUAN",
            "LLANADA VIEJA DE SAN JUAN LAS PARCELAS",
            "LOMAS DE AYACUCHO SECTOR A,SAN FRANCISCO DE ASIS",
            "LOMAS DE AYACUCHO SECTOR F Y E",
            "LOMAS DE AYACUCHO SECTOR UNION Y FUERZAS",
            "VISTAMAR",
            "BRISAS DEL VALLE",
            "LA ENSENADA",
            "LA GRAN FAMILIA",
            "LUIS MARIANO",
            "OCIVH LA ENCENADA B",
            "VILLA ESFUERZO-ENSENADA SUR",
            "VILLA MARIA",
            "LALLANADA VIEJA SECTOR NUESTRA SEÑORA DE LA CANDELARIA"
        });

        SeedComunidadesParaComuna(context, "BOLIVARIANA REVOLUCIONARIA Y SOCIALISTA", new[] {
            "CUMANA II",
            "BARRIO BOLIVAR",
            "CUMANA III",
            "EL PINAL",
            "SANTA EDUVIGUES",
            "BOLIVARIANO I",
            "BOLIVARIANO SECTOR LA PLAZA",
            "BOLIVARINO II 19 DE JULIO"
        });

        SeedComunidadesParaComuna(context, "COMUNA DE PRODUCCIÓN Y SERVICIO ROBERT SERRA", new[] {
            "GRAN PARAISO EN ACCION",
            "MALARIOLOGIA",
            "MALARIOLOGIA II (MANUELA SAENZ)",
            "PARCELAMIENTO SANTA INES",
            "SABILAR I",
            "CUMBRES DE BELLA VISTA",
            "NUEVO PROGRESO",
            "SABILAR II",
            "VILLA 5 DE JULIO"
        });

        SeedComunidadesParaComuna(context, "COMUNA SOCIO PRODUCTIVA ECOLOGICA TURISTICA Y DE SERVICIO CASCO CENTRAL UNIDO", new[] {
            "NUEVO RENACER",
            "AVENIDA EL ISLOTE",
            "BUENA VISTA",
            "ECO TURISTICO PLAZA DEL ESTUDIANTE",
            "QUINTA LOS IBARRA",
            "QUINTA SAN JOSE",
            "RIVERAS DEL MANZANARES",
            "ANTONIO JOSE DE SUCRE-CENTRO",
            "PUERTO RICO",
            "REENCUENTRO ALTAGRACIANO",
            "RIO MANZANARES",
            "LA MATICA DE ALTAGRACIA"
        });

        SeedComunidadesParaComuna(context, "COMUNA URBANA SOCIOPRODUCTIVA SEMILLEROS SOBERANO CANCAMURE", new[] {
            "LA FLORESTA",
            "NUEVA ANDALUCIA",
            "SAN JOSE",
            "URBANIZACION LOS CHAGUARAMOS",
            "URBANIZACION SAN MIGUEL",
            "JOSE MARIA VARGAS DE SANTA LUCIA",
            "LOS COCOS, SECTOR LA CANCHA",
            "LOS COCOS, SECTOR LAS CASITAS",
            "LOS VETERANOS",
            "ARAGUANEY",
            "BUCARE",
            "CIUDAD SALUD",
            "NUEVA CUMANA LAS CASITAS"
        });

        SeedComunidadesParaComuna(context, "SIMON BOLIVAR", new[] {
            "CERRO LOS CACHOS",
            "URB FUNDASUCRE",
            "LA PAZ DE DIOS",
            "LA SABANITA",
            "CASCAJAL VIEJO I",
            "URB ROMULO GALLEGO",
            "30 DE JULIO",
            "BANCO OBRERO",
            "CASCAJAL VIEJO",
            "CASCAJAL VIEJO II"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA DE PRODUCCION INTEGRAL DEL SUR MARISCAL SUCRE", new[] {
            "27 DE SEPTIEMBRE LA INVASION",
            "VILLA SINAY",
            "BRASIL SUR SEGUNDA ETAPA LAS CHARAS",
            "CONSTITUYENTE",
            "RENACER BOLIVARIANO",
            "LA ARBOLEDA",
            "LUISA CACERES DE ARISMENDI",
            "UNIDAD Y VICTORIA",
            "27 DE NOVIEMBRE",
            "HEROES CUMANESES",
            "LAS 2 VILLAS",
            "NUEVA CONSTITUCION",
            "PARCELAMIENTO LA ESPERANZA"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA DE PRODUCCIÓN Y SERVICIOS LA LUCHA ESFUERZO SOBERANO POR VENEZUELA", new[] {
            "CALLE 11",
            "CALLE 11 LA L",
            "DEMOCRACIA",
            "BRASIL, SECTOR 01, MANZANA C",
            "MANUELA SAENZ",
            "MANZANA REVOLUCIONARIA",
            "MERCAL",
            "BRASIL SECTOR 1 MANZANA 5 A",
            "LOS PRESENTES",
            "SAN JOSE",
            "NEGRO PRIMERO",
            "LUZ Y AMOR"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA DE PRODUCCIÓN Y SERVICIOS UN NUEVO AMANECER REVOLUCIONARIO ERNESTO CHE GUEVARA", new[] {
            "ANTONIO JOSE",
            "BRASIL SUR I",
            "BRASIL SUR II",
            "PROGRESISTA",
            "SANTA ANA",
            "VENCEDORES II",
            "ELBOMBEO",
            "MANGUITO PRIMERO",
            "MI REFUGIO",
            "BRASIL SUR SECTOR 3",
            "DIVINO NIÑO"
        });

        SeedComunidadesParaComuna(context, "SOCIO PRODUCTIVA Y DE SERVICIO 3 DE FEBRERO MARISCAL SUCRE", new[] {
            "CASIMBA",
            "LA CIUDADELA",
            "NUEVA TOLEDO",
            "RIBERAS DEL MANZANAREZ",
            "UNIDOS VENCEREMOS",
            "VILLA VENEZIA",
            "CRUZ ROJA"
        });

        SeedComunidadesParaComuna(context, "SOCIO PRODUCTIVA Y DE SERVICIOS BEBEDERO POTENCIA", new[] {
            "BEBEDERO IV",
            "BEBEDERO MANZANA III",
            "VILLA OLIMPICA SECTOR B",
            "CALLE PERIFERICA",
            "CAMPAÑA ADMIRABLE",
            "VENCEDORES POR SIEMPRE",
            "VILLA OLIMPICA",
            "VILLA ROSA",
            "VIVIREMOS Y VENCEREMOS",
            "BEBEDERO I"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA COMANDANTE ETERNO POR SIEMPRE", new[] {
            "ANTONIO GUZMAN BLANCO",
            "GUZMAN BLANCO LAS CASITAS",
            "BARRIO UNIVERSITARIO",
            "VILLA BOLIVARIANA",
            "BARRIO LA LUCHA",
            "COLINAS DE SABATER",
            "LA DEMOCRACIA",
            "MARANATHA"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA DE BIENES Y SERVICIOS MAMÁ ROSA", new[] {
            "AMIGOS DEL BLOQUE 45",
            "BARRIO VENEZUELA",
            "C C UNIDOS EN ACCION",
            "LOS 7 EN ACCION",
            "LOS ROQUES",
            "PROCO",
            "SUPER BLOQUE 46,47,48, CD"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA EL LEGADO DEL COMANDANTE", new[] {
            "MARIA DE SAN JOSE",
            "PEQUEÑA VENECIA SECTOR LAS CASITAS",
            "PEQUEÑA VENECIA SECTOR LOS RANCHOS",
            "SECTOR 1 MANZANA C",
            "SECTOR 1 MANZANA E",
            "SECTOR 4 AREA 1",
            "SECTOR 4 AREA 2",
            "4 DE MARZO",
            "CAMBIO DE RUMBO",
            "SECTOR 1 MANZANA A",
            "SECTOR 1 MANZANA B",
            "SECTOR 1 MANZANA D",
            "VILLA VICTORIA",
            "VIRGEN DEL VALLE III"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA EQUIDAD JUSTICIA SOCIAL SAN LUIS GONZAGA", new[] {
            "AVENIDA PANAMERICANA",
            "VISION DE FUTURO",
            "FE Y ALEGRIA SECTOR IV",
            "BLOQUES ROSADOS",
            "FE ALEGRIA SECTOR ISECTOR LAS CASAS",
            "FE Y ALEGRIA, SECTOR III",
            "EL REENCUENTRO DE LA COMUNIDAD 34 AL 42",
            "LOS RANCHOS FE Y ALEGRIA SECTOR II Y III",
            "NUEVO PERU",
            "SERGIO PANDOZI",
            "URBANIZACION FE Y ALEGRÍA, SECTOR II"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA LA PATRIA GRANDE DE SUCRE", new[] {
            "LA LAGUNITA",
            "LA MALAGUEÑA",
            "LOS MOLINOS",
            "GRAN PARAISO",
            "PARAISO",
            "PROMESA DE DIOS",
            "EL VALLE"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA MAISANTA CORAZÓN DE CHÁVEZ", new[] {
            "BENDICION DE DIOS",
            "DIOS CON NOSOTROS",
            "VILLA AYACUCHO",
            "VILLA LA GUARDIA",
            "VILLA MAISANTA",
            "VILLA ROSARIO",
            "VILLA SANTA ANA",
            "VIRGEN DEL VALLE SABILAR I"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA TURISTICA LOS HIJOS DE CHAVEZ", new[] {
            "TERRAZAS DEL SOL",
            "URB SALTO ANGEL",
            "URB LOS ANGELES",
            "AGUA LUZ",
            "ARBOLEDA COUNTRY ARRECIFE",
            "CIUDAD JUSTICIA",
            "CRISTO REDENTOR",
            "LAS VILLAS",
            "LOS TEJADOS",
            "MARIA ROSALES DE OLIVOS",
            "QUINTA REPUBLICA (SUEÑOS DEL MAÑANA)",
            "VILLA DE SUCRE LOS OLIVOS",
            "VILLA DEL CAMPO I",
            "VILLA ROMANA"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA Y DE SERVICIO ALIANZA REVOLUCIONARIA 2 Y 3", new[] {
            "LLANADA MANZANA A",
            "LLANADA MANZANA B",
            "LLANADA MANZANA D",
            "LLANADA MANZANA F",
            "INTEGRACION COMUNAL 12",
            "SECTOR 2 MANZANA 3",
            "SECTOR 2 MANZANA 4",
            "SECTOR 2 MANZANA 5",
            "SECTOR 2 MANZANA 6",
            "LLANADA MANZANA C",
            "NUEVA ESPERANZA MANZANA G",
            "TIRSA TUTA SUCRE MANZANA E",
            "VILLA DEL SUR MANZANA E"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA Y SERVICIOS TRES PICOS HUGO RAFAEL CHAVEZ FRÍAS", new[] {
            "CAMPO CLARO",
            "DOÑA ONDINA",
            "LA PATRIA ES PRIMERO",
            "LOS CORDEROS DE JESUS",
            "NUESTRA SEÑORA DEL CARMEN",
            "NUESTRA SRA DEL VALLE",
            "NVA ERA DE ACUARIO",
            "PARCELA BOLIVARIANA LAS TORRES",
            "TRES PICOS SECTOR SECTOR LA CANCHA",
            "URB VILLA KAMILA",
            "VILLA DE CAMPO II",
            "VILLA DEL SOL AVENIDA PRINCIPAL TRES PICOS",
            "VILLA FRONTADO",
            "COMANDANTE SUPREMO",
            "EL SOBERANO",
            "LA GRAN SABANA",
            "1 DE MAYO 4 DE ABRIL",
            "1 DE MAYO",
            "NUEVA JERUSALEN",
            "JAGUEY DE LUNA"
        });

        // --- PARROQUIA: AYACUCHO ---

        SeedComunidadesParaComuna(context, "AGROTURISTICA UNION DE ACTIVOS REPRESENTANTES DE BARBACOAS UDARBA", new[] {
            "BARBACOAS SECTOR LAS DOS BODEGAS",
            "CUESTA COLORADA I",
            "CUESTA COLORADA II",
            "EL ROBLE LAS DOS PAREDES",
            "PUERTO ESCONDIDO",
            "BARBACOAS ARRIBAS INPARQUES",
            "LAS 2 CASITAS",
            "PLAN DE LA MESA I",
            "PLAN DE LA MESA II"
        });

        SeedComunidadesParaComuna(context, "AGROTURISTICA Y DE SERVICIO UNIÓN EL TACAL", new[] {
            "ALTO DE MACURIN",
            "EZEQUIEL ZAMORA I",
            "EZEQUIEL ZAMORA II",
            "LA MONTAÑITA CASA BLANCA",
            "LA MONTAÑITA LA ESPERENZA",
            "MONTAÑITA PIEDRA AZUL",
            "TACAL EL PUENTE",
            "TACAL I ALCABALA",
            "TACAL I NUEVO TACAL",
            "TACAL II SECTOR A",
            "TACAL II SECTOR B"
        });

        SeedComunidadesParaComuna(context, "COMUNA SOCIALISTA SOCIOPRODUCTIVA CUMANAGOTO POTENCIA", new[] {
            "CUMANAGOTO 3 SECTOR A",
            "CUMANAGOTO 3 SECTOR B",
            "CACIQUE",
            "LA FE",
            "SOMOS TODOS",
            "CUMANAGOTO EN ACCION",
            "CUMANAGOTO NORTE",
            "CUMANAGOTO TODOS UNIDOS"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA Y PRODUCTIVA LÁZARO HERNÁNDEZ", new[] {
            "URB BERMUDEZ B",
            "CONO SUR",
            "PUERTO ESPAÑA",
            "BERMUDEZ A",
            "BERMUDEZ C",
            "EL GUAPO",
            "CONO NORTE",
            "FRANCISCO BERMUDEZ"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA INTEGRACION GUERRERO DE AYACUCHO", new[] {
            "LA TRINIDAD I",
            "LA TRINIDAD II",
            "TRINIDAD III",
            "VARGAS",
            "PLAZA BOLIVAR",
            "RUIZ PINEDA",
            "EL SALADO"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA TURISTICA Y PESQUERA VENCEDORES EN REVOLUCION DE SAN LUIS", new[] {
            "BENDICION DE DIOS",
            "BIENESTAR DE TODOS",
            "BRISAS DE SAN LUIS",
            "LOS UVEROS",
            "MANHATAN",
            "NUESTRO MAR BOLIVARIANO",
            "SAGRADO CORAZON DE JESUS SAN LUS III",
            "SAN LUIS GONZAGA",
            "TIMONEL"
        });

        // --- PARROQUIA: GRAN MARISCAL ---

        SeedComunidadesParaComuna(context, "AGRICOLA KARIÑA FRANJA DEL TURIMIQUIRE", new[] {
            "LA NUEVA SABANA KARIÑA",
            "LAS KARIÑA DE LA POZA DE MACO",
            "PICHINCHA",
            "SAN PEDRO",
            "SAN PEDRO MATURINCITO",
            "SAN PEDRO SECTOR CAMBURAL (LOS KARIÑAS CAMBURAL)",
            "ZURITA",
            "ALGARROBO",
            "CAMPAMENTO"
        });

        SeedComunidadesParaComuna(context, "AGROTURISTICA KARIÑA AMIGOS DE LA MONTAÑA", new[] {
            "CARIÑAS UNIDOS DEL TERRENO",
            "EL CUPEY DE LA SABANA",
            "KARIÑAS DE MUNDO NUEVO",
            "KARIÑA DE LA PACA",
            "VUELTA CULEBRA",
            "COGOLLAR CRIOLLO",
            "EL SACO",
            "INDIGENA KARIÑAS DE COGOLLAR",
            "LAS PUERTAS"
        });

        SeedComunidadesParaComuna(context, "INDIGENA KARIÑA AGROTURISTICA GRAN MARISCAL DE LOS ALTOS DE SUCRE", new[] {
            "19 DE ABRIL",
            "5 DE JULIO",
            "CUPEY 3 DICIEMBRE",
            "EL PALMAR LAS KARINAS",
            "EL PUEBLO",
            "ISLA EL TANQUE",
            "KARIÑA DE LA ISLA",
            "KARIÑA DEL BOQUETE",
            "LA GANCIA",
            "LA MEDIANIA DEL PALMAR",
            "LA REFORMA",
            "LAS VEGAS",
            "UNIDAD DE CAUCAQUITA"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA AGRÍCOLA TURÍSTICA DE PESCA PLAYA COLORADA", new[] {
            "ACEITE PALO EL CRILLO",
            "HOYO NEGRO",
            "KARIÑA ACEITE PALO",
            "LA URBANIZACION PLAYA COLORADA",
            "LAS LAJAS I",
            "LAS LAJAS KARIÑA II",
            "PLAYA COLORADA VISTA MAR NUEVA ESPERANZA"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA AGROTURISTICA UNIDOS DEL EJE COSTERO", new[] {
            "AGUA BLANCA DE ARAPO",
            "ISLA ARAPO",
            "KARIÑAS DE ARAPO",
            "CABEZA DE ARAPO",
            "ARAPITO",
            "PERDOMO",
            "PLAYA EL HORNO-MADERA",
            "VALLECITO",
            "EL CUMBRE",
            "LA CUMBRE KARIÑA"
        });

        // --- PARROQUIA: RAÚL LEONI ---

        SeedComunidadesParaComuna(context, "AGROTURISTICA PESQUERA INDIGENAS DE NURUCUAL", new[] {
            "PLAYA NURUCUAL",
            "SAN ESTEBAN",
            "UN NUEVO AMANECER",
            "PUENTE QUERE QUERE"
        });

        SeedComunidadesParaComuna(context, "AGROTURISTICA PESQUERA KARIÑA CACIQUE MARAGUEY", new[] {
            "EL PAPO",
            "LA PLANTA",
            "LAS ACACIAS",
            "LUIS BELTRAN GONZALEZ",
            "MARAGUEY",
            "LIMONAL",
            "LIMONAL MORICHAL"
        });

        SeedComunidadesParaComuna(context, "COMUNA INDIGENA APACUANA", new[] {
            "COCHAIMA ESTADIO",
            "INDIGENA NUEVO PUEBLO NUEVO",
            "LOS MANGLES",
            "PUEBLO NUEVO III",
            "PUERTO COLON I",
            "CALLE PRINCIPAL",
            "LA ALCABALA",
            "MI ESPERANZA",
            "PUEBLO NUEVO2",
            "QUERQUERE RIO KARIÑA"
        });

        SeedComunidadesParaComuna(context, "COMUNA INDIGENA KARIÑA AGROTURISTICA PESQUERA Y TRANSPORTE GUERRERA Y GUERREROS DE CHAVEZ", new[] {
            "CAMINO VIEJO",
            "CHISPERO",
            "BARRIO NUEVO CANTV",
            "BRISAS DEL MAR",
            "INDIGENA CAMINO VIEJO",
            "KARIÑA DE LAS ROSAS",
            "KARIÑAS PEREZ FEBRES",
            "PEREZ FEBRES",
            "PUNTA COCHAIMA LOS COQUITOS"
        });

        SeedComunidadesParaComuna(context, "INDIGENA AGROTURISTICA LA UNION DE SAN PEDRITO", new[] {
            "LAS CHARAS",
            "MEDIA FLOR",
            "SAN PEDRITO I SECTOR EL NARANJO",
            "SAN PEDRITO II"
        });

        SeedComunidadesParaComuna(context, "INDÍGENA KARIÑA COSTERA PESQUERA COMERCIAL TURÍSTICA", new[] {
            "ALDEA DE PESCADORES",
            "BOCA 2",
            "BOCA KARIÑA GUICAIPURO",
            "ISLA EL NEGRITO",
            "KARIÑA DE LA PLAYA",
            "COCHAIMA"
        });

        SeedComunidadesParaComuna(context, "LOS VALLES KARIÑAS DE LAS SERRANIAS DEL SILENCIO", new[] {
            "COROLLAL",
            "BOTALON",
            "EL MANGUITO-LA CURVA",
            "KARIÑA GUAKAPURO EL MANGUITO",
            "LA YUCA DEL SILENCIO",
            "LAS KARINAS DE YUCATAN"
        });

        // --- PARROQUIA: SAN JUAN ---

        SeedComunidadesParaComuna(context, "AGRO ECOTURÍSTICA Y MINERA RÍO MANZANARES", new[] {
            "BARRANCA",
            "CUMANACOITA",
            "LA VACA",
            "TIGRE",
            "AGUA SANTA II",
            "CAÑIFLE",
            "NUEVA ESPERANZA",
            "CHIRIGUA",
            "LOMAS DE LOS IPURE",
            "MOCHIMITA I",
            "MOCHIMITA II (MOCHIMITA)",
            "RECONSTRUCION DE LOS IPURE",
            "AGUA SANTA I",
            "SAN AGUSTIN",
            "CREADORES DE RIO BRITO",
            "RIO BRITO",
            "GUARIPA"
        });

        SeedComunidadesParaComuna(context, "AGROTURÍSTICA RIBERAS DE TATARACUAL", new[] {
            "COLORADO",
            "LA LAJA",
            "SAN FERNANDO",
            "SAN FERNANDO I",
            "MANCOMUNIDAD",
            "TATARACUAL SECTOR EL CHISPERO",
            "BOCA DE TATARACUAL",
            "MUNEGRO",
            "VALLE NUEVO",
            "ZABANETA"
        });

        SeedComunidadesParaComuna(context, "EL DESPERTAR DE LOS PUEBLOS BOLIVARIANOS SAN JUAN DE MACARAPANA", new[] {
            "CANCAMURE II",
            "LA MAESTRANZA",
            "LA ZONA",
            "LOS RANCHOS",
            "COMPUERTA CHARIGAL",
            "PUNTA BRAVA",
            "VEGA DE LIMON"
        });

        SeedComunidadesParaComuna(context, "INTEGRAL BOLIVARIANA EL ULTIMO HOMBRE A CABALLO", new[] {
            "CANCAMURE I",
            "CAMPO LINDO",
            "CAOBANAL",
            "MACARAPANA",
            "SAN JUAN VIEJO"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA AGROECOTURISTICA REVOLUCIONARIOS DE GUARANACHE", new[] {
            "GUARANACHE I",
            "MUNDO NUEVO",
            "PECHUGA",
            "SOLEDAD",
            "SANTA MARTA",
            "GUARANACHE 3"
        });

        SeedComunidadesParaComuna(context, "SOCIALISTA AGROTURÍSTICA SAN JUAN BAUTISTA", new[] {
            "GUATACARAL",
            "LA CEIBA",
            "LAS VEGAS",
            "LOS ALTOS DE TRES PICO",
            "LOS ANDES",
            "LOS IPURES",
            "MANCOMUNIDAD",
            "MAS QUE AMOR FRENECI",
            "RECRUZAMORA"
        });

        // --- PARROQUIA: SANTA INÉS ---

        SeedComunidadesParaComuna(context, "AGRO-MINERA PRODUCTIVA Y DE SERVICIOS BOCA DE SABANA SOMOS TODOS", new[] {
            "CARDONAL I",
            "CARDONAL II",
            "EL BULEVAR",
            "GUADALUPE",
            "LA SANDERS",
            "LAS FLORES Y EL RINCON",
            "PRINCIPAL",
            "RIO CARIBE",
            "INAM BELLO MONTE",
            "INAM CAMPECHE",
            "INAM SIMON BOLIVAR",
            "CARDONAL III"
        });

        SeedComunidadesParaComuna(context, "AGROPRODUCTIVA TURISTICA DE SERVICIOS URBANA CENTRO HISTORICO", new[] {
            "GRAN MARISCAL DE AYACUCHO",
            "SANTA CATALINA",
            "ALTO PROCERES",
            "CHICLANA",
            "ANTILLANO EL TANQUE",
            "CALLE LICETT",
            "CALLE NUEVA",
            "LOS CARACAS",
            "MIRADOR ABAJO",
            "MIRADOR LOS PORTALES",
            "VILLA HEROICA",
            "COLINAS DE CORPORIENTE",
            "LAS PARCELAS",
            "CALLEJON GALLEGO",
            "CENTRO HISTORICO",
            "MIRADOR ARRIBA",
            "MIRADOR VALLECITO",
            "MUNDO NUEVO",
            "MUNDO NUEVO 3 SECTORES",
            "MUNDO NUEVO SANTA INES",
            "SAN FRANCISCO HISTORICO",
            "SAN FRANCISCO I",
            "SANFRANCISCO III"
        });

        SeedComunidadesParaComuna(context, "AGRO-PRODUCTIVA Y DE SERVICIOS SUCRE POTENCIA", new[] {
            "ANDRE ELOY BLANCO",
            "BRISAS DE AEROPUERTO",
            "GUARAPICHE NORTE",
            "GUARAPICHE SUR",
            "DOÑA ELENA",
            "LA VILLA ESPERANZA MANUELITA SAENZ",
            "VILLA ROCIO*"
        });

        SeedComunidadesParaComuna(context, "AGROTURISTICA Y DE SERVICIO LAS CATACUMBAS DE SANTA INES", new[] {
            "CLAVELLINO",
            "LA VUELTA DE SALSIPUEDES",
            "LOS ROBLES",
            "RANCHERIA",
            "SALSIPUEDES II"
        });

        SeedComunidadesParaComuna(context, "AGRO-TURISTICA-MINERA NELLYS CALLES", new[] {
            "BRISA DE PANTANILLO",
            "COLINA DE PUERTO DE MADERA",
            "GAMERO",
            "LOS CAUTAROS",
            "PUERTA DE LA MADERA",
            "VALLE DE SANTA INES",
            "BARRANQUIN",
            "LOS FRAILES DE PANTANILLO",
            "ORTIZ",
            "PANTANILLO",
            "CARDONAL DE PANTANILLO",
            "VIRGEN DEL VALLE"
        });

        SeedComunidadesParaComuna(context, "BOLIVARIANA SOCIALISTA AGROTURÍSTICA PRODUCTIVA EL ARBOL DE LAS 3 RAICES", new[] {
            "CRUZ DE LA UNION BAJO SECO",
            "CRUZ DE LA UNION EL CHACO",
            "CRUZ DE LA UNION LA QUEBRADAD",
            "LA ISLA",
            "BRISAS DEL PARAISO",
            "EZEQUIEL ZAMORA",
            "ISLA DEL MANZANARES",
            "SAN JOSE",
            "SIMON RODRIGUEZ",
            "VILLA SAN FRANCISCO DE MIRANDA"
        });

        SeedComunidadesParaComuna(context, "CAMPECHE SOMOS TODOS", new[] {
            "CAMPECHE 4 A",
            "CAMPECHE SECT.II",
            "LINDA DE CAMPECHE",
            "CAMPECHE III",
            "FUERZA BOLIVARIANA",
            "SANTA BARBARA",
            "VILLA CAMPESTRE",
            "ALDEA BOLIVARIANA NUESTRA SEÑORA DEL CARMEN",
            "BRISA DE CAMPECHE",
            "CAMPECHE SECTOR I",
            "COLINA DE CAMPECHE",
            "VILLA CARIBE AZUL"
        });

        SeedComunidadesParaComuna(context, "COMUNA DE SERVICIOS TURISTICOS AGROURBANA COMANDANTE ETERNO", new[] {
            "CALLE I Y II",
            "CHARA DE CANTARRANA",
            "CHARRA DE SAN JOSE",
            "FUERZA DEL MANZANAREZ",
            "VILLA BOLIVARIANA",
            "VILLA CANTARRANA",
            "VILLA PROVIDENCIA",
            "CERRO SABINO",
            "LA GRANJA",
            "SAN CARLOS Y LA BLOQUERA",
            "SANTA HELENA TOW HOUSE",
            "VILLA CALIN",
            "3ERA CALLE",
            "DON TOMAS",
            "LA SABANA",
            "SAN JOSE DE CAMINO NUEVO",
            "VILLA JARDIN",
            "BRISA MAR",
            "FRANCISCO MARTINEZ",
            "LAS CUÑAS",
            "SANTA EDUVIGIS",
            "VILLA MARTHA"
        });

        SeedComunidadesParaComuna(context, "TURÍSTICA PESQUERA VENCEDORES DE SANTA INÉS", new[] {
            "PARCELAMIENTO MIRANDA\" A Y B\"",
            "RIO VIEJO I",
            "DIQUE NUEVO",
            "LA QUINTA",
            "VILLA BICENTENARIO",
            "VILLA PATRICIO",
            "IGNACIO ARENA",
            "RIO DEL VALLE",
            "SACRAMENTO I",
            "SACRAMENTO II",
            "CAUCAGUITA",
            "CORAZON DE JESUS",
            "SACRAMENTO III",
            "CLAP 2021",
            "RIO VIEJO II",
            "BIEN AVENTURADA CON DIOS",
            "DIQUE VIEJO",
            "RAFAEL URDANETA",
            "SAN ANTONIO",
            "ALDEA DE PESCADORES LAS PALOMAS"
        });

        // --- PARROQUIA: VALENTÍN VALIENTE ---

        SeedComunidadesParaComuna(context, "COMUNA DE PRODUCCION INTEGRAL MARIA RODRIGUEZ", new[] {
            "BOSQUE I",
            "URB EL BOSQUE 2",
            "VILLA BELLA",
            "7SOLES",
            "CIUDAD JARDIN",
            "CONJ RES GRAN MARISCAL DE AYACUCHO (500)",
            "CONJ RES GRAN MARISCAL DE AYACUCHO",
            "CONJ RES GRAN MARISCAL DE AYACUCHO PUNTA DEL ESTE",
            "SALVADOR ALIENDRE",
            "VILLA CARIÑO"
        });

        SeedComunidadesParaComuna(context, "COMUNA SOCIO PRODUCTIVA GUERREROS EN VALENTIN GARCIA", new[] {
            "AVENIDA CARUPANO",
            "BRISAS DE MAR",
            "COLINAS DE CAIGUIRE",
            "EL POZO",
            "EL RINCON",
            "LA CARABELA",
            "LA ESPERANZA",
            "LAS DELICIAS",
            "MARINA I",
            "NAVIMCA",
            "NUEVA CUBA"
        });

        SeedComunidadesParaComuna(context, "PRODUCTIVA INTEGRAL CACIQUE CAYAURIMA", new[] {
            "URB CRISTOBAL COLON 4",
            "URB CRISTOBAL COLON 1 Y 2",
            "URB CRISTOBAL COLON 3",
            "URB CRISTOBAL COLON 5",
            "URBANISMO CARIBE",
            "VILLA FELICIDAD I",
            "VILLA FELICIDAD II Y III"
        });

        SeedComunidadesParaComuna(context, "PUEBLO NUEVO", new[] {
            "LA COPITA",
            "LOS GRANEROS; SECTOR SANTA ROSA",
            "LOS CHAIMAS BLOQUEZ",
            "VELA DE CORO",
            "LOS MANGLES",
            "BARBUDO",
            "PARCELAMIENTO LAS GAVIOTAS",
            "PARCELAMIENTO MIRANDA C Y D",
            "TERESITA DE SUCRE",
            "LOS CHAIMAS LAS CASAS"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA BATALLADORES DE CAIGUIRE", new[] {
            "LAS PANCHAS",
            "PEPITONAS",
            "CEMENTERIO",
            "FUNDACION MENDOZA",
            "JUNTOS SOMOS MAS",
            "ANTONIO PATRICIO",
            "LAS TERRAZAS CUMANESAS",
            "VALLE VERDE",
            "ASOVEUNCA",
            "EL MANGLAR",
            "LA GRUTA VIRGEN DEL VALLE",
            "SAN MARTIN",
            "CAMPO ALEGRE I",
            "CAMPO ALEGRE MONTE PIEDAD",
            "SAN MIGUEL DE ARCANGEL",
            "CHISPERO"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA BRISAS DEL GOLFO", new[] {
            "LINDEROS DE AEROPUERTO",
            "SANTA ANA CALLE PRINCIPAL",
            "LA MATICA",
            "CHAVEZ CORANZON DE MI PATRIA",
            "BRISAS DE LA PAZ",
            "BRISAS DE SUCRE",
            "BRISAS DEL PORVENIR",
            "LA GRAN BATALLA",
            "NUEVAS BRISAS"
        });

        SeedComunidadesParaComuna(context, "SOCIOPRODUCTIVA EL PEÑON ESFUERZO DE CHAVEZ", new[] {
            "BRISAS DEL PEÑON II",
            "LUIS CESAR MILLAN",
            "NUEVA TOLEDO",
            "PEÑON ABAJO",
            "PEÑON ARRIBA II",
            "PEÑON I",
            "VILLA DORADA",
            "VILLA SOCIALISTA",
            "14 DE OCTUBRE",
            "18 DE ABRIL",
            "EL MANGLAR",
            "LA GRAN SABANA",
            "PUNTA BAJA",
            "EZEQUIEL ZAMORA",
            "LA PRADERA I Y II",
            "LAS GARZAS",
            "VILLA DELICIA"
        });

        context.SaveChanges();
    }

    private static void SeedComunidadesParaComuna(ApplicationDbContext context, string comunaNombre, string[] comunidades)
    {
        var comuna = context.Comunas.FirstOrDefault(c => c.Nombre == comunaNombre);
        if (comuna == null) return;

        foreach (var nombre in comunidades)
        {
            if (!context.Comunidades.Any(c => c.Nombre == nombre && c.ComunaId == comuna.Id))
            {
                // Generar Guid fijo basado en nombre y comunaId
                var deterministicGuid = CreateGuidFromName(nombre + comuna.Id.ToString());
                context.Comunidades.Add(new Comunidad
                {
                    Id = deterministicGuid,
                    Nombre = nombre,
                    ComunaId = comuna.Id
                });
            }
        }
    }

    private static Guid CreateGuidFromName(string name)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(name));
            return new Guid(hash);
        }
    }
}
