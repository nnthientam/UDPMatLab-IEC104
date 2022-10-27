Interface for data exchange between Simulink Desktop Real-Time and external SCADA systems via IEC 60870-5-104 protocol
This repository provides a tool that allows you to organize data exchange between the Simulink Desktop Real-Time tool and external SCADA systems that support the IEC 60870-5-104 protocol.

A detailed description of the project is given in an article published in the collection of materials of the conference "Energy Through the Eyes of Youth - 2018": https://drive.google.com/file/d/1gMV0PsgVT254y_iv3chqLAyWouDMo85L/view?usp=sharing

The solution was created in Microsoft Visual Studio 2017. Explanation of the projects included in the solution:

UDP104 - Interface for data exchange between Simulink Desktop Real-Time and external SCADA systems via IEC 60870-5-104 protocol;
Configurator - Application "Configurator" with a graphical interface;
SimulinkIEC104 - Library of common classes of projects "UDP104" and "Configurator";
lib60870 - Source code for lib60870.NET version 2.1.0 with minor adjustments (https://github.com/mz-automation/lib60870.NET).
License
The source code is distributed under the GNU license (GPLv3) (The text of the license in English: https://www.gnu.org/licenses/gpl-3.0.en.html Translation into Russian: http://rusgpl.ru/)
