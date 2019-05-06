import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(
  require('@fortawesome/free-solid-svg-icons').faEnvelope,
  require('@fortawesome/free-solid-svg-icons').faGraduationCap,
  require('@fortawesome/free-solid-svg-icons').faHome,
  require('@fortawesome/free-solid-svg-icons').faList,
  require('@fortawesome/free-solid-svg-icons').faSpinner,
  // Brands
  require('@fortawesome/free-brands-svg-icons').faFontAwesome,
  require('@fortawesome/free-brands-svg-icons').faMicrosoft,
  require('@fortawesome/free-brands-svg-icons').faVuejs
)

export {
  FontAwesomeIcon
}
