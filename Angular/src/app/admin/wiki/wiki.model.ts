/* -------------------------------------------------------------------------- */
/*                        Product Name: DictionaryEngine                      */
/*                            Author: Mediasoftpro                            */
/*                       Email: support@mediasoftpro.com                      */
/*       License: Read license.txt located on root of your application.       */
/*                     Copyright 2007 - 2020 @Mediasoftpro                    */
/* -------------------------------------------------------------------------- */

export interface IAPIOptions {
  load: string;
  getinfo: string;
  action: string;
  proc: string;
}

export interface WikiEntity {
  id: number;
  term: string;
  term_complete: string;
  description: string;
  replyid: number;
  tags: string;
  userid: number;
}
